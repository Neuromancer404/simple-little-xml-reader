По непонятной мне причине XReader читал не все данные из xml файла поэтому я написал вот такой простенький инструмент. 
Ридер читает все вложенные теги имеющие данные, одиночные и содержащие другие вложенные он пропускает
Пример работы:
```C#
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
```
где document - содержит вложенные теги
На выходе получается список с содержимым каждого узла в виде словаря.
Метод Scratcher берет содержимое первого найденного тега.
```C#
reader.Stracher("info_sys_code")
```
