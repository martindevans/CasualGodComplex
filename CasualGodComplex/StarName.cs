
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MarvellousMarkovModels;
using StrategyList = System.Collections.Generic.List<System.Tuple<float, System.Func<System.Random, string>>>;

namespace CasualGodComplex
{
    public static class StarName
    {
        private static readonly string[] RealStarNames = 
        {
            "Acamar", "Achernar", "Achird", "Acrab",
            "Akrab", "Elakrab", "Graffias", "Acrux",
            "Acubens", "Adhafera", "Adhara", "Ain",
            "Aladfar", "Alamak", "Alathfar", "Alaraph",
            "Albaldah", "Albali", "Albireo", "Alchiba",
            "Alcor", "Alcyone", "Aldebaran", "Alderamin",
            "Aldhafera", "AlDhanab", "Aldhibah", "Aldib",
            "AlFawaris", "Alfecca", "Meridiana", "Alfirk",
            "Algedi", "AlGiedi", "Algenib", "Algieba",
            "Algol", "Algorab", "Alhajoth", "Alhena",
            "Alioth", "Alkaid", "AlKurud", "AlKalb", "AlRai",
            "Alkalurops", "AlKaphrah", "Alkes", "Alkurah", "Almach",
            "AlMinliar", "AlAsad", "AlNair", "Alnasl", "Alnilam",
            "Alnitak", "Alniyat", "Alphard", "Alphecca", "Alpheratz", "Alrai",
            "Alrakis", "Alrami", "Alrischa", "Alruccbah", "Alsafi",
            "Alsciaukat", "Alshain", "Alshat", "Altair", "Altais",
            "Altarf", "Alterf", "AlThalimain", "Aludra", "Alula",
            "Alwaid", "Alya", "Alzir", "Ancha", "Angetenar", "Ankaa",
            "Antares", "Arcturus", "Arich", "Arided", "Arkab", "Armus", "Arneb",
            "Arrakis", "Ascella", "Asellus", "Ashlesha", "Askella", "Aspidiske",
            "Asterion", "Asterope", "Atik", "Atlas", "Atria", "Auva", "Avior", "Azaleh",
            "Azelfafage", "Azha", "Azimech", "Azmidiske",

            "Baham", "Biham", "Baten", "Kaitos", "Becrux", "Beid", "Bellatrix",
            "Benetnasch", "Betelgeuse", "Botein", "Brachium",

            "Canopus", "Capella", "Caph", "Chaph", "Caphir", "Caput", "Medusae", "Castor",
            "Castula", "Cebalrai", "Celbalrai", "Ceginus", "Celaeno", "Chara", "Cheleb",
            "Chertan", "Chort", "Cor", "Caroli", "Hydrae", "Leonis", "Scorpii", "Serpentis",
            "Coxa", "Cujam", "Caiam", "Cursa", "Cynosura",

            "Dabih", "Decrux", "Deneb", "Denebola", "Dheneb", "Diadem", "Diphda",
            "Dnoces", "Dschubba", "Dubhe", "Duhr",

            "Edasich", "Electra", "Elmuthalleth", "Elnath", "Eltanin", "Enif",
            "Errai", "Etamin",

            "Fomalhaut", "FumalSamakah", "Furud",

            "Gacrux", "Garnet", "Gatria", "Gemma", "Gianfar", "Giedi", "GienahGurab", "Girtab",
            "Gomeisa", "Gorgonea", "Graffias", "Grafias", "Grassias", "Grumium",

            "Hadar", "Hadir", "Haedus", "Haldus", "Hamal", "Hassaleh", "Hydrus", "Heka", "Heze",
            "Hoedus", "Homam", "Hyadum", "Hydrobius",

            "Izar",

            "Jabbah", "Jih",

            "Kabdhilinan", "Kaffaljidhma", "Kajam", "Kastra", "Kaus", "Media", "Keid", "Kitalpha",
            "Kleeia", "Kochab", "Kornephoros", "Kraz", "Ksora", "Kullat", "Kuma",

            "Lanx", "Librae", "Superba", "Lesath", "Lucida",

            "Maasym", "Mahasim", "Maia", "Marfark", "Marfik", "Markab", "Matar", "Mebsuta", "Megrez",
            "Meissa", "Mekbuda", "Menchib", "Menkab", "Menkalinan", "Menkar", "Menkent", "Menkib",
            "Merak", "Merga", "Merope", "Mesarthim", "Miaplacidus", "Mimosa", "Minchir", "Minelava",
            "Minkar", "Mintaka", "Mira", "Mirach", "Miram", "Mirfak", "Mirzam", "Misam", "Mizar",
            "Mothallah", "Muliphein", "Muphrid", "Murzim", "Muscida", "Muscida", "Muscida",

            "Nair", "Naos", "Nash", "Nashira", "Navi", "Nekkar", "Nembus", "Neshmet", "Nihal", "Nunki",
            "Nusakan",

            "Okul",

            "Peacock", "Phact", "Phad", "Pherkad", "Pleione", "Polaris", "Pollux", "Porrima", "Praecipua",
            "Procyon", "Propus", "Proxi", "Pulcherrim",

            "Rana", "Ras", "Rasalas", "Rastaban", "Thaoum", "Regor", "Regulus", "Rigel", "Rigil", "Rijl",
            "Rotanev", "Ruchba", "Ruchbah", "Rukbat",

            "Sabik", "Sadachbia", "Sadalbari", "Sadalmelik", "Sadalsuud", "Sadatoni", "Sadira", "Sadr",
            "Sadlamulk", "Saiph", "Saiph", "Salm", "Sargas", "Sarin", "Sceptrum", "Scheat", "Scheddi",
            "Schedar", "Segin", "Seginus", "Sham", "Shaula", "Sheliak", "Sheratan", "Shurnarkabti", "Shashutu",
            "Sinistra", "Sirius", "Situla", "Skat", "Spica", "Sterope", "Sterope", "Sualocin", "Subra", "Suhail",
            "Suhel", "Sulafat", "Sol", "Syrma",

            "Tabit", "Talitha", "Tania", "Tarazet", "Tarazed", "Taygeta", "Tegmen", "Tegmine", "Terebellum", "Tejat",
            "Thabit", "Theemin", "Thuban", "Tien", "Toliman", "Torcularis", "Tseen", "Turais", "Tyl",

            "Unukalhai", "Unuk",

            "Vega", "Vindemiatrix",

            "Wasat", "Wei", "Wezen", "Wezn",

            "Yed", "Yildun",

            "Zaniah", "Zaurak", "Zavijava", "Zawiat", "Zedaron", "Zelphah", "Zibal", "Zosma", "Zuben", "Zubenelgenubi",
            "Zubeneschamali"
        };

        private static HashSet<string> FictionalLocations = new HashSet<string>
        {
            // Dune (Stars)
            "Ophiuchi", "Eridani", "Alkalurops", "Boötis", "Canopus", "Pavonis", "Fomalhaut",
            "Waiping",  "Gruis", "Shalish", "Niushe", "Shaowei", "Alangue", "Alces",
            "Thalim", "Laoujin",

            // Dune (Planets)
            "Giedi Prime", "Sikun", "Arrakis", "Richese", "Ix", "Caladan", "Salusa", "Anbus", "Dhanab",
            "Buzzell", "Chusuk", "Corrin", "Ecaz", "Gamont", "Gansireed", "Gangishree", "Gammu", "Ginaz", "Grumman", "Hagal", "Harmonthep",
            "Ipyr", "Junction", "Kaitain", "Kolhar", "Lampadas", "Lankiveil", "Lernaeus", "Muritan", "Naraj", "Palma", "Parmentier",
            "Poritrin", "Romo", "Rossak", "Synchrony", "Tleilax", "Tupile", "Wallach IX", "Zanovar",

            // Star Trek
            "Cygni", "Ursae", "Acamar","Aldebaran","Alfin-Bernard","Alnitak","Alpha Ataru","Centauri",
            "Omicron","Shiro","Altair","Amargosa","Amleth","Antares","Antede","Arakon","Arcturus","Argelius","Argos","Atalia",
            "Avenal","Avery","Azati","Eridani","Tauri","Orionis","Alnitak","Rigil", "Kentaurus","Toliman", "Aquilae","Altair", "Amleth",
            "Scorpii","Antede","Antede","Arakon","Bootis","Arcturus","Argelius","Argos","Atalia","Avenal","Avery","Azati", "B'hava'el",
            "Barradas","Bellatrix","Benthan","Benzite","Berengaria", "Bersallis","Aquilae","Aurigae","Capricus","Cassius","Coupsic","Geminorum","Lankal",
            "Lyrae","Magellan","Mahoga","Niobe","Portolan","Reilley","Renner","Simmons","Stromgren", "Thoridar","Betelgeuse","Bilana","Bilaren","Bolarus",
            "Bopak","Boraal","Borratas","Braslota","Bre'el","Breen","Bringloid","Browder","Caldos","Calindra","Callinon","Camor","Canopus","Capella",
            "Cardassia","Carraya","Carson","Ceti","Chess-Wilson","Chin'toka","Clarus","Coltar","Cordannas","Coridan","Corvan","Dameron","Delb","Rana","Deneb",
            "Denkir","Detrian","Devidia","Devolin","Devron","Diomedian","Dopa","Dorala","Draygo","Dreon","Echevarria","El-Adrel","Eminiar","Endicor",
            "Indi","Mynos","Silar","Errikang","Falla","Fima","Hydra","Trianguli","Ganino","Garth","Gaspar","Gavara","'s Star","Goralis","Goren","Grenna",
            "Groombridge","Hanoli","Hanon","Hemakek","Hayashi","Hoek","Iczerone", "Stimson","Idran","Ilecom","Indri","Ingraham","Geminorum","Irtok","Itamish",
            "Izar","J-25","Japori","Kabrel","Kaelon","Kazis","Keloda","Kelsin","Kelvas","Kendi","Kolarus","Koralis","Kotanka","Krios","K'ushui","L-370","L-374",
            "Lalande","Lapolis","Ligon","Loval","Luyten","Maranga","Maxia","Mericor","Mintaka","Mira","Antlia","Moab","Modea","Monac","Murasaki","Narendra","Nel",
            "Nelvana","Nequencia","Norcadia","Norpin","Nyria","Ohniaka","Sagitta","Ceti","Orellius","Orias","Orion","Ovion","Pallas","Paradas","Pegos","Pelosa",
            "Pendari","Pentath","Pernaia","Phylos","Portas","Prema","Procyon","Questar","Ramatis","Regulus","Rigel","Romii","Ross 154","Rubicun","Sahndara","Sappora","Sarona",
            "Sarpeid","Secarus","Shelia","Sheliak","Draconis","Nesterowitz","Sirius","Sol","Solais","Solarion","Soukara","Stillwell","Strnad","Tambor Beta-6","Talos","Tanuga",
            "Tartaras","Cygna","Tavlihna","Tehara","Tellun","Teplan","Terlina","Tessen","Bowles",
            "Mees","T'lli","Topin","Trialas","Trivas","Tsugh", "Kaidnn","Tycho","Tyra","Tyrus","Ufandi","Unefra", "Unroth","Vacca","Vadris","Valo","Vandor",
            "Vega","Vegan","Verath","Veridian","Veytan","Vico","Vilmoran","Vinri","Vissia","Volan","Wolf","Wyanti","Xendi", "Kabu",
            "Orionis","Orionis","Alshain","Menkalinam","Pollux","Sheliak", "Orionis","Browder","Carinae","Aurigae","Alhajoth","Cygni","Gemiorum","Propus","Bootis",
            "Canis","Leonis","Orionis","Lyrae","Alsafi","Canis","Helios","Lyrae","Alnitak",
            "Bajoran","C-111","Carinae","Deneb","Nyrian","Lyrae","Alnitak",

            // Foundation
            "Cygni", "Alpha", "Anacreon", "Arcturus", "Askone", "Asperta", "Aurora", "Baronn", "Bonde", "Cil", "Cinna", "Comporellon", "Daribow",
            "Derowd", "Eos", "Erythro", "Euterpe", "Florina", "Fomalhaut", "Gaia", "Andromeda", "Getorin", "Glyptal", "Haven", "Helicon", "Hesperos",
            "Horleggor", "Ifni", "Iss", "Jennisek", "Kalgan", "Konom", "Korell", "Korell", "Libair", "Livia", "Locris", "Lyonesse", "Lystena", "Mandress",
            "Melpomenia", "Mnemon", "Mores", "Nephelos", "Lingane", "Rhodia", "Tyrann", "Neotrantor", "Nexon", "Nishaya", "Ophiuchus", "Orsha", "Pallas",
            "Radole", "Rhampora", "Rhea", "Rossem", "Salinn", "Santanni", "Sarip", "Sark", "Sayshell", "Sirius", "Siwenna", "Smitheus", "Smushyk", "Smyrno",
            "Solaria", "Synnax", "Tazenda", "Terel", "Terminus", "Trantor", "Vega", "Vincetori", "Voreg", "Wanda", "Wencory", "Whassalian", "Zoranel",

            // Cosmere
            "Ashyn", "Braize", "Nalthis", "Roshar", "Scadrial", "Sel", "Taldain", "Yolen", "Threnody",

            // Honor Harrington
            "Air", "Alizon", "Alphane", "Ameta", "Andermani", "Asgard", "Barnett", "Basilica", "Bassingford", "Beowulf", "Casimir", "Congo", "Dresden", "Durandel",
            "DuQuesne", "Elysia", "Endicott", "Enki", "Erewhon", "Flax", "Grayson", "Gregor", "Grendelsbane", "Gryphon", "Hades", "Halliman",
            "Hamilton", "Hancock", "Haven", "Hope", "Kemal", "Ki-Rin", "Kornati", "Kreskin", "Kuan Yin", "Lois", "Lovat", "Lynx", "MacGregor", "Manticore",
            "Marsh", "Masada", "Matapan", "Maya", "Medusa", "Melungeon", "Meyerdahl", "Mesa", "Mfecane", "Midgard", "Midsummer", "Monica", "Montana",
            "Ndebele", "Nagasaki", "Dijon", "Hamburg", "Geneva", "Octagon", "Parmley", "Phoenix", "Pontifex", "Potsdam", "Prague",
            "Refuge", "Rembrandt", "Shuttlesport", "Sidemore", "Sphinx", "Torch", "Toulon", "Zanzibar", "Zulu",
        };

        private static readonly Model MarkovNameModel = 
            new ModelBuilder(3)
                .Teach(RealStarNames)
                .Teach(FictionalLocations)
            .ToModel();

        private static HashSet<string> GreekLetters = new HashSet<string>
        {
            "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omnicron", "Pi", "Rho",
            "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega"
        };

        private static HashSet<string> Decorators = new HashSet<string>
        {
            "Major", "Majoris", "Minor", "Minoris", "Prime", "Secundis", "System" 
        }; 

        private static HashSet<string> SpecialLocations = new HashSet<string>()
        {
            "Epsilon Eridani", "San Martin", "Seaford Nine", "Proctor Three", "Smoking Frog", "First of the Sun", "Xendi Sabu", "Bela Tegeuse"
        };

        private static HashSet<string> Names = new HashSet<string>
        {
            "Trevor","Yeltsin","Barnard","Genovese", "Martin", "Simon", "Rob", "Ed", "Walter", "Mohammed", "Emil", "Shannon", "Nicole", "Yury",
            "Coleman","Deanne","Rosenda","Geoffrey","Taryn","Noreen","Rita","Jeanetta","Stanton","Alesha","Mark","Georgiann","Modesta","Lee",
            "Cyndi","Raylene","Ellamae","Sharmaine","Candra","Karine","Trena","Tarah","Dorie","Kyoko","Wei","Cristopher","Yoshie","Meany","Zola",
            "Corene","Suzie","Sherwood","Monnie","Savannah","Amalia","Lon","Luetta","Concetta","Dani","Sharen","Mora","Wilton","Hunter","Nobuko",
            "Maryellen","Johnetta","Eleanora","Arline","Rae","Caprice"
        };

        private static readonly StrategyList _namingStrategies = new StrategyList
        {
            Weighted( 1.0f, PlainMarkov ),
            Weighted( 1.0f, WithDecoration(1.0f, WithDecoration(0.001f, PlainMarkov)) ),
            Weighted( 0.05f, r => Letter(r) + "-" + Integer(r)),
            Weighted( 0.01f, NamedStar ),
            Weighted( 0.01f, r => r.RandomChoice(SpecialLocations) )
        };

        private static readonly StrategyList _prefixStrategies = new StrategyList
        {
            Weighted( 1.0f, Greek ),
            Weighted( 1.0f, Decorator ),
            Weighted( 0.01f, RomanNumeral ),
            Weighted( 1.0f, Letter ),
            Weighted( 1.0f, Integer ),
            Weighted( 0.3f, Decimal ),
            Weighted( 0.0f, r => "Al" ),
            Weighted( 0.0f, r => "San" ),
        };

        private static readonly StrategyList _suffixStrategies = new StrategyList
        {
            Weighted( 1.0f, Greek ),
            Weighted( 1.0f, Decorator ),
            Weighted( 1.0f, RomanNumeral ),
            Weighted( 1.0f, Letter ),
            Weighted( 1.0f, Integer ),
            Weighted( 0.3f, Decimal ),
        };


        // Naming Strategies
        private static string PlainMarkov(Random random)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var name = MarkovNameModel.Generate(random);
            return textInfo.ToTitleCase(name);
        }

        private static string NamedStar(Random random)
        {
            return random.RandomChoice(Names) + "'s Star";
        }

        // Prefix and Suffix Strategies
        private static string Greek(Random random)
        {
            return random.RandomChoice(GreekLetters);
        }

        private static string Decorator(Random random)
        {
            return random.RandomChoice(Decorators);
        }

        private static string RomanNumeral(Random random)
        {
            var integer = (Int32)random.NormallyDistributedSingle(10, 15, 1, 200);
            var bigInteger = (Int32) random.NormallyDistributedSingle(400, 100, 200, 3000);
            return ToRoman(random.RandomChoice(0.8f) ? integer : bigInteger);
        }

        private static string Letter(Random random)
        {
            var letter = (char)random.Next(65, 90);
            return letter.ToString();
        }

        private static string Integer(Random random)
        {
            var number = (int)random.NormallyDistributedSingle(100, 5, 1, 1000);;
            return number.ToString();
        }

        private static string Decimal(Random random)
        {
            var number = random.NormallyDistributedSingle(100, 5, 1, 1000); ;
            return number.ToString("N2");
        }

        // Modifiers
        private static Func<Random, string> WithDecoration(float probability, Func<Random, string> func)
        {
            return (r) =>
            {
                var result = func(r);
                if (r.NextDouble() > probability)
                    return result;


                var prefix = r.WeightedChoice(_prefixStrategies)(r) + " ";
                var suffix = " " + r.WeightedChoice(_suffixStrategies)(r);

                switch (
                    r.RandomChoice(new List<Tuple<float, string>>()
                    {
                        Tuple.Create(0.4f, "neither"),
                        Tuple.Create(1.0f, "prefix"),
                        Tuple.Create(1.0f, "suffix"),
                        Tuple.Create(0.2f, "both")
                    }).Item2)
                {
                    case "prefix":
                        return prefix + result;
                    case "suffix":
                        return result + suffix;
                    case "both":
                        return prefix + result + suffix;
                    default:
                        return result;
                }
            };
        }

        // Helpers
        private static string ToRoman(int number)
        {
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new InvalidOperationException();
        }

        private static bool RandomChoice(this Random random, float bias = 0.5f)
        {
            return random.NextDouble() < bias;
        }

        private static Tuple<float, Func<Random, string>> Weighted(float f, Func<Random, string> s)
        {
            return Tuple.Create(f, s);
        }

        private static T RandomChoice<T>(this Random random, IEnumerable<T> choices)
        {
            var choiceList = choices.ToList();
            var count = choiceList.Count();
            var index = random.Next(0, count);
            return choiceList.ElementAt(index);
        }

        private static T WeightedChoice<T>(this Random random, List<Tuple<float, T>> choices)
        {
            var totalWeight = choices.Sum(x => x.Item1);
            var choice = random.NextDouble()*totalWeight;
            return choices.First(c =>
            {
                choice -= c.Item1;
                return (choice <= 0);
            }).Item2;
        }

        public static string Generate(Random random)
        {
            return random.WeightedChoice(_namingStrategies)(random);
        }

        public static IEnumerable<string> Generate(Random random, int count)
        {
            var choices = new HashSet<string>();
            while (choices.Count < count)
            {
                var newChoice = Generate(random);
                if (choices.Add(newChoice)) yield return newChoice;
            }
        }
    }
}
