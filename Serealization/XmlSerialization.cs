using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Serealization
{
  // Xml-сериализатор
  public class XmlSerialization<T> : ISerializer<T>
  {
    public string Serialize(T request)
    {
      var stringBuilder = new StringBuilder();
      XmlSerializer serializer = new XmlSerializer(typeof(T)); //System.InvalidOperationException
      using (var xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings { OmitXmlDeclaration = true }))
        serializer.Serialize(xmlWriter, request, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
      return stringBuilder.ToString();
    }

    public T Deserialize(string request)
    {
      var serializer = new XmlSerializer(typeof(T));
      using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(request)))
      return (T)serializer.Deserialize(stream);
    }
  }
}

//using System;
//using System.IO;
//using System.Xml.Serialization;

//namespace Serialization
//{
//  // класс и его члены объявлены как public
//  [Serializable]
//  public class Person
//  {
//    public string Name { get; set; }
//    public int Age { get; set; }

//    // стандартный конструктор без параметров
//    public Person()
//    { }

//    public Person(string name, int age)
//    {
//      Name = name;
//      Age = age;
//    }
//  }
//  class Program
//  {
//    static void Main(string[] args)
//    {
//      // объект для сериализации
//      Person person = new Person("Tom", 29);
//      Console.WriteLine("Объект создан");

//      // передаем в конструктор тип класса
//      XmlSerializer formatter = new XmlSerializer(typeof(Person));

//      // получаем поток, куда будем записывать сериализованный объект
//      using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
//      {
//        formatter.Serialize(fs, person);

//        Console.WriteLine("Объект сериализован");
//      }

//      //// десериализация
//      //using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
//      //{
//      //  Person newPerson = (Person)formatter.Deserialize(fs);

//      //  Console.WriteLine("Объект десериализован");
//      //  Console.WriteLine("Имя: {0} --- Возраст: {1}", newPerson.Name, newPerson.Age);
//      //}

//      Console.ReadLine();
//    }
//  }
//}