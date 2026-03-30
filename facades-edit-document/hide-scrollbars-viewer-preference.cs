using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Hide UI elements such as scrollbars for a cleaner display on small screens
            doc.HideWindowUI = true;
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden UI to '{outputPath}'.");
    }
}