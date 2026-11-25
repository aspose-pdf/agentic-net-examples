using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string outputPath = "comparison.pdf";

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

        // Load both PDFs with deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Use default side‑by‑side comparison options
            var options = new SideBySideComparisonOptions();

            // SideBySidePdfComparer is a static class; call its static Compare method
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
        }

        Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
    }
}
