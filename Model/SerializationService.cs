using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendar.Model
{
    public static class SerializationService
    {
        private static readonly String _sourcePath = "../../EventsDB.xml";

        public static IEnumerable<EventModel> ReadSource()
        {
            if(File.Exists(_sourcePath))
            {
                using(var fs = new FileStream(_sourcePath, FileMode.Open))
                {
                    if(fs.Length > 0)
                    {
                        var bf = new BinaryFormatter();
                        return (IEnumerable<EventModel>)bf.Deserialize(fs);
                    }
                }
            }
            return null;
        }

        public static void WriteToSource(IEnumerable<EventModel> eventsList)
        {
            if(eventsList != null)
            {
                using (var fs = new FileStream(_sourcePath, FileMode.OpenOrCreate))
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(fs, eventsList);
                }
            }
        }
    }
}
