using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string jsonReportPath = "diffReport.json";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDF documents
        Document firstDoc = new Document(firstPdfPath);
        Document secondDoc = new Document(secondPdfPath);

        // Perform a text‑based comparison that returns DiffOperation objects
        var diffOperations = TextPdfComparer.CompareDocumentsPageByPage(
            firstDoc,
            secondDoc,
            new ComparisonOptions()
        );

        // Generate a JSON diff report and save it to a file
        JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
        jsonGenerator.GenerateOutput(diffOperations, jsonReportPath);

        Console.WriteLine($"JSON diff report saved to '{jsonReportPath}'.");
    }
}
