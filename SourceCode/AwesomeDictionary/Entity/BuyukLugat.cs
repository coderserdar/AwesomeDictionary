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
    public class BuyukLugat
    {
        [Column(IsPrimaryKey = true,
            IsDbGenerated = true,
            DbType = "INT NOT NULL Identity",
            CanBeNull = false)]
        public int BuyukLugatId { get; set; }

        [Column]
        public string BuyukLugatName { get; set; }

        [Column]
        public string BuyukLugatMeaning { get; set; }

        [Column]
        public string BuyukLugatNameMeaning { get; set; }

    }
}