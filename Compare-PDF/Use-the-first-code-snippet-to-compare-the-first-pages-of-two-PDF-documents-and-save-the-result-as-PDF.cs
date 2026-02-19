using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the source PDFs and the output file
        string pdfPath1 = "first.pdf";
        string pdfPath2 = "second.pdf";
        string outputPath = "comparison.pdf";

        // Verify that the input files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"Input file not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"Input file not found: {pdfPath2}");
            return;
        }

        // Load the two documents (load rule)
        Document doc1 = new Document(pdfPath1);
        Document doc2 = new Document(pdfPath2);

        // Retrieve the first page from each document (pages are 1‑based)
        Page page1 = doc1.Pages[1];
        Page page2 = doc2.Pages[1];

        // Configure side‑by‑side comparison options (optional)
        SideBySideComparisonOptions options = new SideBySideComparisonOptions
        {
            AdditionalChangeMarks = true // show change marks that appear on other pages
        };

        // Compare the two pages and save the result (comparison API)
        SideBySidePdfComparer.Compare(page1, page2, outputPath, options);

        Console.WriteLine($"Side‑by‑side comparison saved to '{outputPath}'.");
    }
}