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
    public class AlmancaTurkce
    {
        [Column(IsPrimaryKey = true,
            IsDbGenerated = true,
            DbType = "INT NOT NULL Identity",
            CanBeNull = false)]
        public int AlmancaTurkceId { get; set; }

        [Column]
        public string AlmancaTurkceName { get; set; }

        [Column]
        public string AlmancaTurkceMeaning { get; set; }

        [Column]
        public string AlmancaTurkceNameMeaning { get; set; }

    }
}