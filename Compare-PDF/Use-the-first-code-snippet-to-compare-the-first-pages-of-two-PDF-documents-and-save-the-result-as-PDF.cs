using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF files – adjust paths as needed
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";

        // Output file for the side‑by‑side comparison result
        const string outputPath = "result.pdf";

        // Verify that both source files exist
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

        try
        {
            // Load the two PDF documents
            Document doc1 = new Document(pdfPath1);
            Document doc2 = new Document(pdfPath2);

            // Retrieve the first page from each document (Aspose.Pdf uses 1‑based indexing)
            Page firstPageDoc1 = doc1.Pages[1];
            Page firstPageDoc2 = doc2.Pages[1];

            // Configure side‑by‑side comparison options (optional)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Example: show additional change marks
                // AdditionalChangeMarks = true
            };

            // Compare the two pages and save the result as a PDF
            SideBySidePdfComparer.Compare(firstPageDoc1, firstPageDoc2, outputPath, options);
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}