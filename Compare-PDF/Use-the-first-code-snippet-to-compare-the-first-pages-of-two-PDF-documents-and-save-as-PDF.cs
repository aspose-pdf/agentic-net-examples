using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the source PDF files and the output file.
        string pdfPath1 = "input1.pdf";
        string pdfPath2 = "input2.pdf";
        string outputPath = "output.pdf";

        // Verify that both input files exist.
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load the two PDF documents.
        Document doc1 = new Document(pdfPath1);
        Document doc2 = new Document(pdfPath2);

        // Ensure each document contains at least one page.
        if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
        {
            Console.Error.WriteLine("One of the documents does not contain any pages.");
            return;
        }

        // Retrieve the first page from each document (Aspose.Pdf uses 1‑based indexing).
        Page firstPageDoc1 = doc1.Pages[1];
        Page firstPageDoc2 = doc2.Pages[1];

        // Create default side‑by‑side comparison options.
        SideBySideComparisonOptions options = new SideBySideComparisonOptions();

        // Perform the comparison; the method writes the result directly to the specified output file.
        SideBySidePdfComparer.Compare(firstPageDoc1, firstPageDoc2, outputPath, options);

        Console.WriteLine($"Side‑by‑side comparison saved to: {outputPath}");
    }
}