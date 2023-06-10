namespace UpSchool.Domain.Utilities
{
    internal class LocalDB : ILocalDB
    {
        public List<string> IPs { get; set; }

        public LocalDB()
        {
            IPs = new List<string>();
        }
    }
}
