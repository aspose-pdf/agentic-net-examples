using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1   = "doc1.pdf";
        const string pdfPath2   = "doc2.pdf";
        const string docOutput  = "documents_comparison.pdf";
        const string pageOutput = "first_pages_comparison.pdf";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // -------------------------------------------------
                // Document‑level side‑by‑side comparison
                // -------------------------------------------------
                SideBySideComparisonOptions docOptions = new SideBySideComparisonOptions
                {
                    // Show change marks that appear on other pages (optional)
                    AdditionalChangeMarks = true,
                    // Use the default comparison mode (ignore spaces)
                    ComparisonMode = ComparisonMode.IgnoreSpaces
                };

                // The Compare method writes the result directly to the specified file
                SideBySidePdfComparer.Compare(doc1, doc2, docOutput, docOptions);

                // -------------------------------------------------
                // Page‑level side‑by‑side comparison (first page of each document)
                // -------------------------------------------------
                // Ensure the documents have at least one page
                if (doc1.Pages.Count > 0 && doc2.Pages.Count > 0)
                {
                    Page page1 = doc1.Pages[1]; // 1‑based indexing
                    Page page2 = doc2.Pages[1];

                    SideBySideComparisonOptions pageOptions = new SideBySideComparisonOptions
                    {
                        // Example: do not include additional change marks for page comparison
                        AdditionalChangeMarks = false,
                        ComparisonMode = ComparisonMode.Normal
                    };

                    SideBySidePdfComparer.Compare(page1, page2, pageOutput, pageOptions);
                }
            }

            Console.WriteLine($"Document comparison saved to '{docOutput}'.");
            Console.WriteLine($"First‑page comparison saved to '{pageOutput}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF comparison: {ex.Message}");
        }
    }
}