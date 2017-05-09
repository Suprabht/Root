using System;

namespace DataAccessLayer
{
    public class Class1
    {
        public void init()
        {
            using (var db = new BridgeToCareContext())
            {
                //var blogs = db.AspNetUsers
                //    .Where(b => b.Rating > 3)
                //    .OrderBy(b => b.Url)
                //    .ToList();
            }
        }
    }
}
