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
    public class OxfordEnglishEnglish
    {
        [Column(IsPrimaryKey = true,
            IsDbGenerated = true,
            DbType = "INT NOT NULL Identity",
            CanBeNull = false)]
        public int OxfordId { get; set; }

        [Column]
        public string OxfordName { get; set; }

        [Column]
        public string OxfordMeaning { get; set; }

        [Column]
        public string OxfordNameMeaning { get; set; }

    }
}