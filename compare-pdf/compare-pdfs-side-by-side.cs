using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "docA.pdf";
        const string pdfPath2 = "docB.pdf";
        const string resultPath = "comparison.pdf";

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
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();
                // The default options are sufficient for aligning pages of different sizes.
                SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
                Console.WriteLine($"Comparison saved to '{resultPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
