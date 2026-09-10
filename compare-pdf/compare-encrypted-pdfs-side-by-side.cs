using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF files
        const string firstPdfPath  = "encrypted1.pdf";
        const string secondPdfPath = "encrypted2.pdf";
        // Passwords for the encrypted PDFs (user passwords)
        const string firstPassword  = "userPass1";
        const string secondPassword = "userPass2";
        // Path where the comparison result will be saved
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load the encrypted documents by providing the passwords to the constructors
        using (Document doc1 = new Document(firstPdfPath, firstPassword))
        using (Document doc2 = new Document(secondPdfPath, secondPassword))
        {
            // Create comparison options (default settings)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform side‑by‑side comparison and save the result PDF
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}