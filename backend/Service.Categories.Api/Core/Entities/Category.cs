namespace Service.Categories.Api.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int? IdParentCategory { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? SoftDelete { get; set; }
    }
}
