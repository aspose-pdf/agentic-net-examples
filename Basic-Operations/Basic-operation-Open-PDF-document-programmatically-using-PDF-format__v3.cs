using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "copy.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document. The using block ensures proper disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example operation: display basic information.
            Console.WriteLine($"Pages: {pdfDoc.Pages.Count}");
            Console.WriteLine($"Author: {pdfDoc.Info.Author}");

            // Save a copy of the opened document in PDF format.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Document opened and saved to '{outputPath}'.");
    }
}