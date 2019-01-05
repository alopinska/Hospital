using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Hospital_Data
{
    public static class DataDispatcher
    {
        static string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "StaffData.dat";
        public static T DeserializeData<T>()
        {
            
            BinaryFormatter bf = new BinaryFormatter();

            try
            {                
                using (Stream str = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return (T)bf.Deserialize(str);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Błąd deserializacji", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return default(T);
        }

        public static void SerializeData<T>(T variable)
        {
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                using (Stream str = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    bf.Serialize(str, variable);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Błąd serializacji", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }  
        
        public static Employee CloneBySerialization(Employee emp)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Employee));

            string serializedObject;
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, emp);
                serializedObject = textWriter.ToString();
            }
            StringReader strReader = new StringReader(serializedObject);
            return (Employee)xmlSerializer.Deserialize(strReader);
        }      



    }
}
