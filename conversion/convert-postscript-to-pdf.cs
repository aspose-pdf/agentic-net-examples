using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.ps";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PostScript file using the dedicated PsLoadOptions.
        PsLoadOptions loadOptions = new PsLoadOptions();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPath, loadOptions))
        {
            // Saving without explicit SaveOptions always produces a PDF.
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"PostScript file converted to PDF: '{outputPath}'.");
    }
}