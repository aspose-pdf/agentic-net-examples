using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Edit standard document properties via DocumentInfo
            doc.Info.Title   = "New Document Title";
            doc.Info.Author  = "John Doe";
            doc.Info.Subject = "Sample Subject";
            doc.Info.Keywords = "Aspose, PDF, Metadata";

            // Alternatively, set the title using the dedicated method
            doc.SetTitle("New Document Title");

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document properties updated and saved to '{outputPath}'.");
    }
}