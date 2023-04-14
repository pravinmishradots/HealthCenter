using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Core
{
    public class DataTableServerSide
    {
        public List<DataTableColumns> columns { get; set; }
        public int draw { get; set; }
        public int length { get; set; }
        public List<DataTableOrder> order { get; set; }
        public DataTableSearch search { get; set; }
        public List<DataTableMultiSearch> multisearch { get; set; }
        public DataTableCustomFilter filter { get; set; }
        public int start { get; set; }
        public DataTableFilterType filterType { get; set; }
    }

    public class DataTableColumns
    {
        public int data { get; set; }
        public string name { get; set; }
        public int orderable { get; set; }
        public DataTableSearch search { get; set; }
        public int searchable { get; set; }
    }

    public class DataTableSearch
    {
        public bool regex { get; set; }
        public string value { get; set; }
    }

    public class DataTableMultiSearch
    {
        public string column { get; set; }
        public DataTableFilterType filter { get; set; }
        public string value { get; set; }
        public bool withOr { get; set; }
    }

    public class DataTableCustomFilter
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class DataTableOrder
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class CustomOrderBy
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public enum DataTableFilterType
    {
        Contains = 0,
        Equals = 1,
        StartsWith = 2,
        LessThanOrEqual = 3,
        GreaterThanOrEqual = 4
    }
}
