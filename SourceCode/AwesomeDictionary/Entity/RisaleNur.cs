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
    public class RisaleNur
    {
        [Column(IsPrimaryKey = true,
            IsDbGenerated = true,
            DbType = "INT NOT NULL Identity",
            CanBeNull = false)]
        public int RisaleNurId { get; set; }

        [Column]
        public string RisaleNurName { get; set; }

        [Column]
        public string RisaleNurMeaning { get; set; }

        [Column]
        public string RisaleNurNameMeaning { get; set; }

    }
}