using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF files
        const string pdfPath1 = "encrypted1.pdf";
        const string pdfPath2 = "encrypted2.pdf";
        // Passwords for the encrypted PDFs (user passwords)
        const string password1 = "userPass1";
        const string password2 = "userPass2";
        // Path where the comparison result will be saved
        const string resultPath = "comparison_result.pdf";

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
            // Open the first encrypted document with its password
            using (Document doc1 = new Document(pdfPath1, password1))
            // Open the second encrypted document with its password
            using (Document doc2 = new Document(pdfPath2, password2))
            {
                // Configure comparison options (default options are sufficient for a basic comparison)
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Perform side‑by‑side comparison and save the result PDF
                SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
            }

            Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}