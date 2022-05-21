using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Azure; 
using Azure.Search.Documents.Indexes;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

namespace AzureSearchUtils.SaveIndexDefinition;
internal class SaveIndexDefinitionUtil
{
    #region Properties

    private readonly IConfiguration _config;  

    #endregion

    #region Constructor

    internal SaveIndexDefinitionUtil(IConfiguration config)
    {
        _config = config;
    }

    #endregion

    public void SaveIndexDefinitionFile(string indexName, string filePath)
    {
        try
        {
            int fieldCount = 0;
            string azureSearchUrl = _config["AzureSearchUrl"];
            string key = _config["AzureSearchAdminKey"];

            SearchIndexClient searchIndexClient = new SearchIndexClient(new Uri(azureSearchUrl), new AzureKeyCredential(key));

            var indexDefinition = searchIndexClient.GetIndex(indexName);

            var indexToSave = new IndexDefinition()
            {
                Name = indexName
            };

            foreach(var field in indexDefinition.Value.Fields)
            {
                var i = new IndexField();
                i.Name = field.Name;
                i.Type = field.Type.ToString();
                i.Searchable = field.IsSearchable;
                i.Filterable = field.IsFilterable;
                i.Sortable = field.IsSortable;
                i.Facetable = field.IsFacetable;
                i.Key = field.IsKey;
                i.IndexAnalyzer = field.IndexAnalyzerName.ToString();
                i.SearchAnalyzer = field.SearchAnalyzerName.ToString();
                i.Analyzer = field.AnalyzerName.ToString();

                indexToSave.IndexFields.Add(i);

                fieldCount++;
                Console.WriteLine($"Field # {fieldCount} - {i.Name} - added.");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Persisting index definition to file: {filePath}");

            var jsonToSave = JsonConvert.SerializeObject(indexToSave);
            File.WriteAllText(filePath, jsonToSave.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"MACKLEMORE!! An exception occured.");
            Console.WriteLine(ex.ToString() + Environment.NewLine + ex.StackTrace);
        }
    }
}
