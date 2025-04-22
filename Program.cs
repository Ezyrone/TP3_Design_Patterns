using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Backupfiles
{
    // Interface 
    public interface IFile
    {
        public string getcontent();

        void Save(string path);
    }

    
    public class TexteFile : IFile
    {
        private string _content;
        public string getcontent() => _content;

        public TexteFile(string content)
        {
            _content = content;
        }

        public virtual void Save(string path)
        {
            File.WriteAllText(path, _content);
            Console.WriteLine($"File saved : {path}");
        }
    }

    // Décorateurs
    public abstract class DecoratorFile : IFile
    {
        protected IFile _file;
        public string content => _file.getcontent();

        string getcontent() => _file.getcontent();

        public DecoratorFile(IFile file)
        {
            _file = file;
        }

        public virtual void Save(string path)
        {
            _file.Save(path);
        }

        string IFile.getcontent()
        {
            return getcontent();
        }
    }

    
    public class CompressionDecorator : DecoratorFile
    {
        public CompressionDecorator(IFile file) : base(file) {
            int n = file.getcontent().Length;

            for (int i = 0; i < n; i++) {

                int count = 1;
                while (i < n - 1 && file.getcontent()[i] == file.getcontent()[i + 1]) {
                    count++;
                    i++;
                }

                Console.Write(file.getcontent()[i]);
                Console.Write(count);

            }

        }

        public override void Save(string path)
        {
            base.Save(path);
            Console.WriteLine("→ Compressed");
        }
    }

    
    public class EncryptedDecorator : DecoratorFile
    {
        public EncryptedDecorator(IFile file) : base(file) 
        {
            string result;

            char[] result  = new char[file.getcontent.Length];

            foreach (char c in file.getcontent()) {

                
                
                
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