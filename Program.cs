using System;
using System.IO;

namespace SauvegardeFichiers
{
    // Interface commune
    public interface IFichier
    {
        void Enregistrer(string chemin);
    }

    // Classe de base : FichierTexte
    public class FichierTexte : IFichier
    {
        private string _contenu;

        public FichierTexte(string contenu)
        {
            _contenu = contenu;
        }

        public virtual void Enregistrer(string chemin)
        {
            File.WriteAllText(chemin, _contenu);
            Console.WriteLine($"Fichier texte enregistré : {chemin}");
        }
    }

    // Classe de base pour les décorateurs
    public abstract class FichierDecorator : IFichier
    {
        protected IFichier _fichier;

        public FichierDecorator(IFichier fichier)
        {
            _fichier = fichier;
        }

        public virtual void Enregistrer(string chemin)
        {
            _fichier.Enregistrer(chemin);
        }
    }

    // Décorateur : Compression
    public class CompressionDecorator : FichierDecorator
    {
        public CompressionDecorator(IFichier fichier) : base(fichier) {}

        public override void Enregistrer(string chemin)
        {
            base.Enregistrer(chemin);
            Console.WriteLine("→ Compression du fichier (simulation)");
        }
    }

    // Décorateur : Chiffrement
    public class ChiffrementDecorator : FichierDecorator
    {
        public ChiffrementDecorator(IFichier fichier) : base(fichier) {}

        public override void Enregistrer(string chemin)
        {
            base.Enregistrer(chemin);
            Console.WriteLine("→ Chiffrement du fichier (simulation)");
        }
    }

    // Pattern Facade : SauvegardeManager
    public class SauvegardeManager
    {
        public void SauvegarderFichier(string chemin, string contenu)
        {
            IFichier fichier = new FichierTexte(contenu);
            fichier = new CompressionDecorator(fichier);
            fichier = new ChiffrementDecorator(fichier);

            fichier.Enregistrer(chemin);
            Console.WriteLine("✔️ Sauvegarde complète via SauvegardeManager");
        }
    }

    // Point d'entrée
    class Program
    {
        static void Main(string[] args)
        {
            string contenu = "Ceci est un fichier très important.";
            string chemin1 = "sauvegarde_directe.txt";
            string chemin2 = "sauvegarde_facade.txt";

            Console.WriteLine("=== Test avec décorateurs manuels ===");
            IFichier fichier = new FichierTexte(contenu);
            fichier = new CompressionDecorator(fichier);
            fichier = new ChiffrementDecorator(fichier);
            fichier.Enregistrer(chemin1);

            Console.WriteLine("\n=== Test via Facade ===");
            SauvegardeManager manager = new SauvegardeManager();
            manager.SauvegarderFichier(chemin2, contenu);
        }
    }
}