using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "encrypted1.pdf";
        const string pdf2Path = "encrypted2.pdf";
        const string password1 = "userPassword1";
        const string password2 = "userPassword2";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the encrypted PDFs providing the respective passwords
        using (Document doc1 = new Document(pdf1Path, password1))
        using (Document doc2 = new Document(pdf2Path, password2))
        {
            // Optional: configure comparison options (default values are sufficient for a basic run)
            ComparisonOptions options = new ComparisonOptions();

            // Perform a side‑by‑side comparison and save the visual result to a PDF file
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, new SideBySideComparisonOptions());

            // If you need the raw list of differences instead of a visual PDF, you could use:
            // var diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);
            // Console.WriteLine($"Number of differences: {diffs.Count}");
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}