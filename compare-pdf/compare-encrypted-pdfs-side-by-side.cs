using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "encrypted1.pdf";
        const string pdfPath2 = "encrypted2.pdf";
        const string password1 = "userPass1";
        const string password2 = "userPass2";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        try
        {
            // Load the encrypted PDFs by providing passwords to the constructors
            using (Document doc1 = new Document(pdfPath1, password1))
            using (Document doc2 = new Document(pdfPath2, password2))
            {
                // ---------- Text‑based comparison (gives diff operations) ----------
                var diffs = TextPdfComparer.CompareDocumentsPageByPage(
                    doc1,
                    doc2,
                    new ComparisonOptions()
                );

                bool isEqual = diffs.All(pageDiff => pageDiff.Count == 0);
                int differencesCount = diffs.Sum(pageDiff => pageDiff.Count);

                Console.WriteLine($"Documents are equal: {isEqual}");
                Console.WriteLine($"Number of differences: {differencesCount}");

                // ---------- Visual side‑by‑side comparison (produces a PDF) ----------
                const string visualDiffPath = "comparison_result.pdf";
                SideBySidePdfComparer.Compare(
                    doc1,
                    doc2,
                    visualDiffPath,
                    new SideBySideComparisonOptions()
                );
                Console.WriteLine($"Visual comparison saved to '{visualDiffPath}'.");
            }
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
