using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSearchUtils.SaveIndexDefinition;
public class IndexDefinition
{
    public string? Name { get; set; }

    public List<IndexField> IndexFields { get; set; } = new List<IndexField>();
}
