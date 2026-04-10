using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonDemo
{
    static void Main()
    {
        // Input PDF files with different page sizes
        const string pdfPath1 = "documentA.pdf";
        const string pdfPath2 = "documentB.pdf";
        const string resultPath = "comparisonResult.pdf";

        // Verify that input files exist
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
            // Load the first PDF document
            using (Document doc1 = new Document(pdfPath1))
            // Load the second PDF document
            using (Document doc2 = new Document(pdfPath2))
            {
                // Configure side‑by‑side comparison options (defaults are sufficient for alignment)
                SideBySideComparisonOptions options = new SideBySideComparisonOptions
                {
                    // Example: show additional change marks if needed
                    AdditionalChangeMarks = true
                };

                // Perform the comparison; the result PDF will contain pages from both documents
                // placed side by side, handling differing page dimensions automatically.
                SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
            }

            // Verify that the result file was created
            if (File.Exists(resultPath))
            {
                Console.WriteLine($"Comparison completed successfully. Result saved to '{resultPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Comparison completed but result file was not found.");
            }
        }
        catch (ArgumentException ex)
        {
            // This exception is thrown if the comparison encounters unsupported conditions
            Console.Error.WriteLine($"Argument error during comparison: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}