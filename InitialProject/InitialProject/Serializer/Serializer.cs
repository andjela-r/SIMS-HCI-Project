using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;

namespace InitialProject.Serializer
{
    class Serializer<T> where T: ISerializable, new()
    {
        private const char Delimiter = '|';
        private const char ListDelimiter = ',';

        public void ToCSV(string fileName, List<T> objects)
        {
            StringBuilder csv = new StringBuilder();

            foreach(T obj in objects)
            {
                if(obj.GetType() == typeof(List))
                {
                    string list = string.Join(ListDelimiter, obj.ToCSV());
                    csv.AppendLine(list);
                }

                string line = string.Join(Delimiter.ToString(), obj.ToCSV());
                csv.AppendLine(line);
            }

            File.WriteAllText(fileName, csv.ToString());

        }

        public List<T> FromCSV(string fileName)
        {
            List<T> objects = new List<T>();

            foreach(string line in File.ReadLines(fileName))
            {
                string[] csvValues = line.Split(Delimiter);
                T obj = new T();
                obj.FromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }
    }
}
