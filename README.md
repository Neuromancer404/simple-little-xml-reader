# simple-little-xml-parser

По непонятной мне причине XReader не читал весь объем данных из xml файлов поэтому был написан такой маленький ридер. Он писался для массивов данных персональной информации и подходит для документов типа: 

<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root>
  <info_sys_code>000</info_sys_code>
  <documents>
    <document>
      <citizen>
        <name></name>
        ...
      </citizen>
    </document>
  </documents>
</root>

Пример работы:
coolXmlReader reader = new coolXmlReader();
if(reader.Create(path, "document"))
{
  List<Dictionary<string, string>> data = reader.Read();
  foreach(Dictionary<string, string> item in data)
  {
    Person person = new Person();
    foreach(string key in item.Keys)
    {
      person.personFieldFill(key, item[key], logWriter);
    }
    persons.Add(person);
    }
  writeFile(persons, reader);
  }
  else
  {
    logWriter.logWriting("Количество открывающих и закрывающих тегов document не одинаково");
  }
  
  Метод Sсratcher возвращает значение из первого встреченного тега (напрмер: reader.Stracher("info_sys_code"))
