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
    public class EnglishTurkishVol1
    {
        [Column(IsPrimaryKey = true,
            IsDbGenerated = true,
            DbType = "INT NOT NULL Identity",
            CanBeNull = false)]
        public int EnglishVol1Id { get; set; }

        [Column]
        public string EnglishVol1Name { get; set; }

        [Column]
        public string EnglishVol1Meaning { get; set; }

        [Column]
        public string EnglishVol1NameMeaning { get; set; }

    }
}