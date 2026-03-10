using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Set standard metadata via DocumentInfo (doc.Info)
            doc.Info.Title = "Sample PDF Title";
            doc.Info.Author = "John Doe";
            doc.Info.Subject = "Demonstration of metadata setting";
            doc.Info.Keywords = "Aspose.Pdf, metadata, example";
            doc.Info.Creator = "My Application";
            doc.Info.Producer = "Aspose.Pdf for .NET";

            // Optionally set creation and modification dates
            doc.Info.CreationDate = DateTime.Now;
            doc.Info.ModDate = DateTime.Now;

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata set and PDF saved to '{outputPath}'.");
    }
}