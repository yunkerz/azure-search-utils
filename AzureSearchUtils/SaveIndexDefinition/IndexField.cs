using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSearchUtils.SaveIndexDefinition;
public class IndexField
{
    public string? Name { get; set; } 

    public string? Type { get; set; }

    public bool? Searchable { get; set; } 

    public bool? Filterable { get; set; }

    public bool? Facetable { get; set; }

    public bool? Sortable { get; set; }

    public bool? Key { get; set; }

    public string? Analyzer { get; set; }

    public string? IndexAnalyzer { get; set; }

    public string? SearchAnalyzer { get; set; }

    // Ignoring SynonymMaps for now.
}
