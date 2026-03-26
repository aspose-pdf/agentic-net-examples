using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "encrypted1.pdf";
        const string secondPdfPath = "encrypted2.pdf";
        const string firstPassword = "user1";
        const string secondPassword = "user2";
        const string resultPath = "comparison_result.pdf";

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

        try
        {
            using (Document document1 = new Document(firstPdfPath, firstPassword))
            using (Document document2 = new Document(secondPdfPath, secondPassword))
            {
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();
                // Options can be customized here if needed, e.g., options.ShowDeletedContent = true;
                SideBySidePdfComparer.Compare(document1, document2, resultPath, options);
                Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
