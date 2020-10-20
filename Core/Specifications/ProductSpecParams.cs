namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int maxPages = 50;
        public int PageIndex { get; set; }=1;
        private int _itemsPerPage= 6;
        public int ItemsPerPage 
        {
            get => _itemsPerPage;
            set => _itemsPerPage = (value > maxPages) ? maxPages : value;
        }

        public string Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}