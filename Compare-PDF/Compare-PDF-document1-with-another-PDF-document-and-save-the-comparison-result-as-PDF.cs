using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: first PDF, second PDF, output PDF
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: program <pdf1> <pdf2> <output>");
            return;
        }

        string pdfPath1 = args[0];
        string pdfPath2 = args[1];
        string outputPath = args[2];

        // Verify that the input files exist
        if (!File.Exists(pdfPath1))
        {
            Console.WriteLine($"Error: File not found – {pdfPath1}");
            return;
        }

        if (!File.Exists(pdfPath2))
        {
            Console.WriteLine($"Error: File not found – {pdfPath2}");
            return;
        }

        try
        {
            // Load the two PDF documents
            Document doc1 = new Document(pdfPath1);
            Document doc2 = new Document(pdfPath2);

            // Configure side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Example option: show additional change marks
                AdditionalChangeMarks = true
            };

            // Perform the comparison; the result is written directly to outputPath
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);

            Console.WriteLine($"Comparison completed. Result saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}