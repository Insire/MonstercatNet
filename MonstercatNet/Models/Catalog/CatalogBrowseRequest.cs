namespace SoftThorn.MonstercatNet
{
    public class CatalogBrowseRequest
    {
        public Catalog[] results { get; set; }
        public object notFound { get; set; }
        public int total { get; set; }
        public int limit { get; set; }
        public int skip { get; set; }
    }
}
