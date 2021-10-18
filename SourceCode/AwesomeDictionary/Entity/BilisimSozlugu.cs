using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeDictionary
{
    [Table]
    public class BilisimSozlugu
    {
        [Column(IsPrimaryKey = true,
            IsDbGenerated = true,
            DbType = "INT NOT NULL Identity",
            CanBeNull = false)]
        public int BilisimSozluguId { get; set; }

        [Column]
        public string BilisimSozluguName { get; set; }

        [Column]
        public string BilisimSozluguMeaning { get; set; }

        [Column]
        public string BilisimSozluguNameMeaning { get; set; }

    }
}