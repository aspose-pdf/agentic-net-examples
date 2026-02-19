using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Directory that contains the input PDFs and where the result will be saved.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file names.
        string firstPdfPath = Path.Combine(dataDir, "first.pdf");
        string secondPdfPath = Path.Combine(dataDir, "second.pdf");

        // Output PDF file name.
        string outputPdfPath = Path.Combine(dataDir, "comparison_result.pdf");

        // Verify that both input files exist.
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {secondPdfPath}");
            return;
        }

        // Load the two documents to be compared.
        Document doc1 = new Document(firstPdfPath);
        Document doc2 = new Document(secondPdfPath);

        // Configure side‑by‑side comparison options.
        SideBySideComparisonOptions options = new SideBySideComparisonOptions
        {
            // Example setting: show change marks that appear on other pages.
            AdditionalChangeMarks = true
        };

        // Perform the side‑by‑side comparison.
        // This method creates the result PDF directly at the specified path.
        SideBySidePdfComparer.Compare(doc1, doc2, outputPdfPath, options);

        // Optionally, load the generated result and save it again using the
        // standard document‑save rule to demonstrate the required save pattern.
        Document resultDoc = new Document(outputPdfPath);
        resultDoc.Save(outputPdfPath); // document-save rule

        Console.WriteLine($"Comparison PDF saved to: {outputPdfPath}");
    }
}