using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create temporary PDF files to guarantee they exist at runtime
        string firstPdfPath = Path.Combine(Path.GetTempPath(), "first.pdf");
        string secondPdfPath = Path.Combine(Path.GetTempPath(), "second.pdf");
        string outputPath = Path.Combine(Path.GetTempPath(), "comparison_result.pdf");

        // First document – simple text
        using (var doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello World"));
            doc.Save(firstPdfPath);
        }

        // Second document – same text but with extra spaces (to be ignored)
        using (var doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello   World"));
            doc.Save(secondPdfPath);
        }

        // Load the PDFs and perform side‑by‑side comparison ignoring spaces
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            var options = new SideBySideComparisonOptions
            {
                ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
        }

        Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
    }
}
