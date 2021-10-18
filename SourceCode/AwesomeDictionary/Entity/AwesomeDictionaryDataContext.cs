using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeDictionary
{
    public class AwesomeDictionaryDataContext : DataContext
    {
        public const string ConnectionString = @"Data Source=isostore:/AwesomeDictionary.sdf; Max Database Size=256; Max Buffer Size=4096;";
        public AwesomeDictionaryDataContext(string connectionString)
            : base(connectionString) { }
        public Table<AlmancaTurkce> AlmancaTurkces;
        public Table<RisaleNur> RisaleNurs;
        public Table<BilisimSozlugu> Bilisims;
        public Table<All> AllNames;
        public Table<Favourite> Favourites;
        public Table<EnglishTurkishVol1> EnglishTurkishVol1s;
        public Table<EnglishTurkishVol2> EnglishTurkishVol2s;
        public Table<BuyukLugat> BuyukLugats;
        public Table<OxfordEnglishEnglish> Oxfords;
        public Table<KelimeAnlamlari> Kelimes;
        public Table<AppSettings> AppSettings;
    }
}