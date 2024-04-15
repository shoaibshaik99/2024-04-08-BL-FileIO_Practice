using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

namespace FileIO_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\smdsh\\source\\repos\\2024-04-08-BL-FileIO_Practice\\FileIO_Practice\\File.txt";
            string filePathToCopy = "C:\\Users\\smdsh\\source\\repos\\2024-04-08-BL-FileIO_Practice\\FileIO_Practice\\File - Copy.txt";

            FilePrograms.FileExists(filePath);

            FilePrograms.OnlyCreateNonExistentFile(filePath);

            string[] LinesFromFile = File.ReadAllLines(filePath);
            foreach (string line in LinesFromFile)
            {
                Console.WriteLine(line);
            }

            string TextFromFile = File.ReadAllText(filePath);
            Console.WriteLine();
            Console.WriteLine(TextFromFile);

            //File,ReadLines(<path>) returns an IEnumerable
            var FileLinesRead = File.ReadLines(filePath).ToList();
            FileLinesRead.Add("A line addes to List of text obtailed from ReadLines Method");
            Console.WriteLine();
            foreach (string line in FileLinesRead)
            {
                Console.WriteLine(line);
            }

            File.Delete(filePathToCopy);//uncomment this if you are running multiple times after you had run it already once.
            File.Copy(filePath, filePathToCopy);
            Console.WriteLine();
            Console.WriteLine("Content in copied file is as follows");
            Console.WriteLine(File.ReadAllText(filePathToCopy));
            
            //cannot print directly from an array
            Console.WriteLine();
            Console.WriteLine(File.ReadAllLines(filePathToCopy));


            File.Delete(filePathToCopy);
            //File.Create(filePathToCopy);
            //File.Delete(filePathToCopy);

            //StreamReader
            Console.WriteLine();
            Console.WriteLine("StreamReader Practice");
            using (StreamReader sr = File.OpenText(filePath))
            {
                string s = sr.ReadLine();
                while (s != null)
                {
                    Console.WriteLine(s);
                    s = sr.ReadLine();
                }
                
                //Console.WriteLine(s = sr.ReadLine());
            }

            //StreamWriter
            Console.WriteLine();
            Console.WriteLine("StreamWriter Practice");
            using(StreamWriter sw  = File.AppendText(filePath))
            {
                sw.WriteLine("The line added via stream writer class");
                sw.Close();
                Console.WriteLine(File.ReadAllText(filePath));
            }

            //Object to CSV
            string directoryPath = @"C:\\Users\\smdsh\\source\\repos\\2024-04-08-BL-FileIO_Practice\\FileIO_Practice";
            string fileName = "CSVdata.csv";
            string csvFilePath = Path.Combine(directoryPath, fileName);
            WriteToCsv(csvFilePath);

            //csv to object?
            ReadFromCSV(csvFilePath);
        }

        public static void WriteToCsv(string filePath)
        {
            List<Person> people = new List<Person>()
            {
                new Person() {Name = "Mukund", Age = 25, Country = "India"},
                new Person() {Name = "Krishna", Age = 29, Country = "USA"},
                new Person() {Name = "Ali", Age = 23, Country = "India"}
            };

            var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            using(CsvWriter csvWriter = new CsvWriter(streamWriter, configPersons))
            {
                csvWriter.WriteRecords(people);
            }
            Console.WriteLine("CSV data added into file");
        }

        public static void ReadFromCSV(string filePath)
        {
            var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (StreamReader streamReader = new StreamReader(filePath))
            using (CsvReader csvReader = new CsvReader(streamReader, configPersons))
            {
                //Read records from csv file
                IEnumerable<Person> records = csvReader.GetRecords<Person>();

                //Process each Record
                foreach(Person person in records)
                {
                    Console.WriteLine($"Name: {person.Name}, Age: {person.Age}, Country: {person.Country}");
                }
            }
        }

    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
    }
}
