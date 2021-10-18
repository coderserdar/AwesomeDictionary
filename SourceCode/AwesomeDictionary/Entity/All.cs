using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Phone.Data.Linq;
// index özelliği aşağıdaki using sınıfı ile etkili bir hale geliyor.
using Microsoft.Phone.Data.Linq.Mapping;

namespace AwesomeDictionary
{
    [Index(Columns = "AllName, AllMeaning, AllNameMeaning, AllNameSource ASC", IsUnique = false, Name = "indAllNames")]
    [Table]
    public class All
    {
        [Column(IsPrimaryKey = true,
            IsDbGenerated = true,
            DbType = "INT NOT NULL Identity",
            CanBeNull = false)]
        public int AllId { get; set; }

        [Column]
        public string AllName { get; set; }

        [Column]
        public string AllMeaning { get; set; }

        [Column]
        public string AllNameMeaning { get; set; }

        [Column]
        public string AllSource { get; set; }

        [Column]
        public string AllNameSource { get; set; }

    }
}