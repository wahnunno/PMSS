namespace Demo1.Models
{
    public class PayrollDataListClass
    {
        public int nMonth { get; set; }
        public int nYear { get; set; }
        public List<PayrollDataClass> lstData { get; set; } = new List<PayrollDataClass>();
        public int PagePrevious { get; internal set; }
        public int PageNext { get; internal set; }
        public int PageNumber { get; internal set; }
        public int PageSize { get; internal set; }
        public int PagerCount { get; internal set; }
        public int TotalCount { get; internal set; }
    }
}
