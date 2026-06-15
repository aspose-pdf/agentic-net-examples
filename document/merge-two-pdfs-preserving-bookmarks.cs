using System;
using System.IO;
using Aspose.Pdf;               // Core API for PDF manipulation

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";   // Path to the first source PDF
        const string secondPdf = "second.pdf";  // Path to the second source PDF
        const string outputPdf = "merged.pdf";  // Path for the merged result

        // Verify that source files exist before proceeding
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        try
        {
            // Load both source PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // Merge the second document into the first.
                // The instance Merge method preserves bookmarks (outlines) from both PDFs.
                doc1.Merge(doc2);

                // Save the merged document as PDF.
                doc1.Save(outputPdf);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}