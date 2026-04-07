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
            // Hide the toolbar and the menu bar when the PDF is opened
            doc.HideToolBar = true;
            doc.HideMenubar = true;

            doc.Save(outputPath);
        }

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}