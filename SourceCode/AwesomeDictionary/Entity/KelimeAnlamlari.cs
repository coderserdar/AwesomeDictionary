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
    public class KelimeAnlamlari
    {
        [Column(IsPrimaryKey = true,
            IsDbGenerated = true,
            DbType = "INT NOT NULL Identity",
            CanBeNull = false)]
        public int KelimeAnlamlariId { get; set; }

        [Column]
        public string KelimeAnlamlariName { get; set; }

        [Column]
        public string KelimeAnlamlariMeaning { get; set; }

        [Column]
        public string KelimeAnlamlariNameMeaning { get; set; }

    }
}