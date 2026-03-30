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
            // Set the viewer preference to hide navigation panels on open
            doc.PageMode = PageMode.UseNone;
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with PageMode UseNone to '{outputPath}'.");
    }
}