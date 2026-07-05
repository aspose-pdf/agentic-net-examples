using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.ps";   // PostScript source file
        const string outputPath = "output.pdf"; // Destination PDF file

        // Verify that the source file exists before attempting conversion
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PostScript file with PsLoadOptions (default settings)
        // The Document constructor performs the conversion to PDF internally.
        using (Document pdfDocument = new Document(inputPath, new PsLoadOptions()))
        {
            // Save the resulting PDF. No SaveOptions are required because
            // Document.Save(string) always writes PDF when no format is specified.
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Conversion completed: '{outputPath}'");
    }
}