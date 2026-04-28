using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF files
        const string pdf1Path = "encrypted1.pdf";
        const string pdf2Path = "encrypted2.pdf";

        // Corresponding passwords for each PDF
        const string password1 = "userPass1";
        const string password2 = "userPass2";

        // Path where the comparison result will be saved
        const string resultPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Open the encrypted PDFs by providing the passwords in the constructors
            using (Document doc1 = new Document(pdf1Path, password1))
            using (Document doc2 = new Document(pdf2Path, password2))
            {
                // Ensure both documents contain at least one page
                if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
                {
                    Console.Error.WriteLine("One of the PDFs does not contain any pages.");
                    return;
                }

                // Initialize side‑by‑side comparison options (default settings)
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Perform side‑by‑side comparison on the first pages and save the result PDF
                SideBySidePdfComparer.Compare(
                    doc1.Pages[1],   // first page of the first document
                    doc2.Pages[1],   // first page of the second document
                    resultPath,
                    options);
            }

            Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}