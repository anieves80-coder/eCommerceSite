namespace EStore.web.Models.Domain
{
    public class ProductsModel
    {

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime PostedDate { get; set; }

        public string PicURL { get; set; }

        public string Category { get; set; }

        public Guid Owner { get; set; }

    }
}
