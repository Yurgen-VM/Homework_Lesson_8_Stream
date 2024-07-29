namespace Task_1
{
    internal class Program
    {
        /*
         
        Написаь утилиту, которая ищет файлы определенного расширения
        с указанным текстом. Рекурсивно. Пример вызова утилиты: utility.exe txt текст.
        
        */     

        static void Main(string[] args)
        {
           
            List<string> list = SearhFile(path: args[0], ext: args[1], text: args[2]);
            if (list.Count == 0)
                Console.WriteLine("Файл не найден");
            else
                Console.WriteLine(String.Join("\n", list));
            Console.WriteLine( "Поиск завершен" );


            Console.ReadLine();
        }

        private static List<string> SearhFile(string path, string ext, string text)
        {
            List<string> list = new List<string>();
            DirectoryInfo di = new DirectoryInfo(path);
            var directories = di.GetDirectories();
            var files = di.GetFiles();            
            
            foreach (var file in files)
            {
                if (ext.Contains(file.Extension)) // Проверяем файлы на соответвие искомому расширению
                {
                    using (StreamReader sr = new StreamReader(file.OpenRead()))
                    {
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            if (line.Contains(text, StringComparison.InvariantCultureIgnoreCase)) // Ищем совпадение текста вне зависимости от регистра
                            {
                                list.Add(file.FullName);
                            }
                        }
                    }
                    return list;
                }
            }
            foreach (var dir in directories)
            {
                list.AddRange(SearhFile(dir.FullName, ext, text));
            }
            
            return list;
        }                
   }
}
