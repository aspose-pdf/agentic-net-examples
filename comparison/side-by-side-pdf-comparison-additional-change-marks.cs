using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "comparison_output.pdf";

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
            // Load both PDFs with deterministic disposal.
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // Configure comparison options to show additional change marks.
                var options = new SideBySideComparisonOptions
                {
                    AdditionalChangeMarks = true
                };

                // Perform side‑by‑side visual comparison (static class, static method).
                SideBySidePdfComparer.Compare(doc1, doc2, outputPdf, options);
            }

            Console.WriteLine($"Comparison PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
