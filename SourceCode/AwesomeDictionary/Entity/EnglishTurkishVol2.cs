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
    public class EnglishTurkishVol2
    {
        [Column(IsPrimaryKey = true,
            IsDbGenerated = true,
            DbType = "INT NOT NULL Identity",
            CanBeNull = false)]
        public int EnglishVol2Id { get; set; }

        [Column]
        public string EnglishVol2Name { get; set; }

        [Column]
        public string EnglishVol2Meaning { get; set; }

        [Column]
        public string EnglishVol2NameMeaning { get; set; }

    }
}