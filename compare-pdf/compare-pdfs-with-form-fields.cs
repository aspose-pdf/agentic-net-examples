using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "input1.pdf";
        const string pdfPath2 = "input2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both source PDFs exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the PDF documents
            Document doc1 = new Document(pdfPath1);
            Document doc2 = new Document(pdfPath2);

            // Side‑by‑side comparison options – form field values are compared by default
            var sideOptions = new SideBySideComparisonOptions();

            // Perform visual side‑by‑side comparison and generate a diff PDF
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, sideOptions);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF comparison: {ex.Message}");
        }
    }
}
