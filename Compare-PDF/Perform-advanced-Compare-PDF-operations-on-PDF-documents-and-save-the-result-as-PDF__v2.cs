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
        const string resultPdf = "comparison_result.pdf";

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
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // Configure side‑by‑side comparison options
                SideBySideComparisonOptions options = new SideBySideComparisonOptions
                {
                    AdditionalChangeMarks = true,               // show change marks from other pages
                    ComparisonMode = ComparisonMode.IgnoreSpaces // ignore spaces during text comparison
                };

                // Perform comparison and save the result as a PDF
                SideBySidePdfComparer.Compare(doc1, doc2, resultPdf, options);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}