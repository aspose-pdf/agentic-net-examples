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
        const string outputPath = "comparison_result.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two source PDFs
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Configure side‑by‑side comparison options
                var options = new SideBySideComparisonOptions
                {
                    AdditionalChangeMarks = true
                };

                // Perform the visual comparison – SideBySidePdfComparer is static
                // and its Compare method returns void and expects 4 arguments.
                SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
            }

            Console.WriteLine($"Side‑by‑side comparison saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
