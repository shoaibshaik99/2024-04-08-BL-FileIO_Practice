namespace FileIO_Practice
{
    public class FilePrograms
    {
        public static bool FileExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine($"{filePath} exists\n");
                return true;
            }
            Console.WriteLine($"{filePath} does not exist\n");
            return false;   
        }

        public static void OnlyCreateNonExistentFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("File already exists hence no action being taken\n");
                return;
            }
            File.Create(filePath);
            Console.WriteLine($"Previously non existent file Created at this file path ---> {filePath}\n");
        }


    }
}
