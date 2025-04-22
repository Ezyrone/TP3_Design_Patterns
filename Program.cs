using System;
using System.IO;

namespace Backupfiles
{
    // Interface 
    public interface IFile
    {
        void Save(string path);
    }

    
    public class TexteFile : IFile
    {
        private string _content;

        public TexteFile(string content)
        {
            _content = content;
        }

        public virtual void Save(string chemin)
        {
            File.WriteAllText(chemin, _content);
            Console.WriteLine($"File saved : {chemin}");
        }
    }

    // Décorateurs
    public abstract class DecoratorFile : IFile
    {
        protected IFile _file;

        public DecoratorFile(IFile file)
        {
            _file = file;
        }

        public virtual void Save(string path)
        {
            _file.Save(path);
        }
    }

    
    public class CompressionDecorator : DecoratorFile
    {
        public CompressionDecorator(IFile file) : base(file) {}

        public override void Save(string path)
        {
            base.Save(path);
            Console.WriteLine("→ Compressed");
        }
    }

    
    public class EncryptedDecorator : DecoratorFile
    {
        public static void EncryptedDecorator(IFile file) : base(file) 
        {
            int n = string.Length;
            for (int i = 0; i < n; i++)
            {
                int count = 1;
                while (i < n - 1 && string[i] == string[i + 1])
                {
                    count++;
                    i++;
                }
                Console.Write(string[i]);
                if (count > 1)
                {
                    Console.Write(count);
                }
            }

        }

        public override void Save(string path)
        {
            base.Save(path);
            Console.WriteLine("→ Encrypted");
        }
    }

    // Facade 
    public class SaveManager
    {
        public void SaveFile(string path, string content)
        {
            IFile file = new TexteFile(content);
            file = new CompressionDecorator(file);
            file = new EncryptedDecorator(file);

            file.Save(path);
            Console.WriteLine("✔️ Save completed");
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            string content = "Important File !";
            string path1 = "Direct_Save.txt";
            string path2 = "save_facade.txt";

            Console.WriteLine("=== Test with manual decorators ===");
            IFile file = new TexteFile(content);
            file = new CompressionDecorator(file);
            file = new EncryptedDecorator(file);
            file.Save(path1);

            Console.WriteLine("\n=== Test Facade ===");
            SaveManager manager = new SaveManager();
            manager.SaveFile(path2, content);
        }
    }
}