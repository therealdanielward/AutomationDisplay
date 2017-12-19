using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestersErrorLog.ViewModel
{
    public class EntityViewModel
    {
        public string Name { get; set; }

        public List<Items> Items { get; set; }
   
        public IEnumerable<Items> pageItems { get; set; }
    }

    public class Items
    {
        public int Id { get; set; }
        public string CompleteDate { get; set; }
        public string URL { get; set; }
        public string StatusLevel { get; set; }
        public string ErrorMessage { get; set; }

    }

    
}