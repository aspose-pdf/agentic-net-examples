using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document. The using block ensures deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Example operation: output the number of pages in the PDF.
            Console.WriteLine($"Page count: {doc.Pages.Count}");

            // Save a copy of the document (PDF format). No SaveOptions needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}