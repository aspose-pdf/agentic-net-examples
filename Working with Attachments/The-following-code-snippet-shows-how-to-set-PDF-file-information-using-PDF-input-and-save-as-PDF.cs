using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF, set metadata, and save
        using (Document doc = new Document(inputPdf))
        {
            // Set document information (metadata)
            doc.Info.Title       = "Sample PDF Title";
            doc.Info.Author      = "John Doe";
            doc.Info.Subject     = "Demonstration of setting PDF metadata";
            doc.Info.Keywords    = "Aspose.Pdf, metadata, example";
            doc.Info.Creator     = "My Application";
            doc.Info.Producer    = "Aspose.Pdf for .NET";

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with updated information to '{outputPdf}'.");
    }
}