namespace SitecoreCookbook.Search
{
    public class Facet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Selected { get; set; }

        public Facet(string id, string name, string selected)
        {
            this.Id = id;
            this.Name = name;
            this.Selected = selected;
        }
        public Facet(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
