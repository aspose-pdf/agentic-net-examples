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

        // Passwords for the encrypted PDFs
        const string password1 = "userPass1";
        const string password2 = "userPass2";

        // Verify that the input files exist
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

        // Load the encrypted PDFs by providing passwords to the Document constructors
        using (Document doc1 = new Document(pdfPath1, password1))
        using (Document doc2 = new Document(pdfPath2, password2))
        {
            // Configure side‑by‑side comparison options (no OutputPath property)
            SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions();
            // Example: you can set additional options here, e.g., compareOptions.HighlightColor = Color.Yellow;

            // Perform the visual side‑by‑side comparison – static class, static method
            SideBySidePdfComparer.Compare(doc1, doc2, "comparison_result.pdf", compareOptions);
        }

        Console.WriteLine("Comparison completed. Result saved to 'comparison_result.pdf'.");
    }
}
