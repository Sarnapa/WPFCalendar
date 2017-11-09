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
    public class SerializationService
    {
        private static readonly String _sourcePath = "../../EventsDB.xml";

        public IEnumerable<Event> ReadSource()
        {
            if(File.Exists(_sourcePath))
            {
                using(var fs = new FileStream(_sourcePath, FileMode.Open))
                {
                    if(fs.Length > 0)
                    {
                        var bf = new BinaryFormatter();
                        return (IEnumerable<Event>)bf.Deserialize(fs);
                    }
                }
            }
            return null;
        }

        public void WriteToSource(IEnumerable<Event> eventsList)
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
