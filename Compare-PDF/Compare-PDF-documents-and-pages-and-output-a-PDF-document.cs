using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expect two input PDF paths and one output PDF path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <firstPdfPath> <secondPdfPath> <outputPdfPath>");
            return;
        }

        string firstPdfPath = args[0];
        string secondPdfPath = args[1];
        string outputPdfPath = args[2];

        // Validate input files.
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two PDF documents.
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Prepare comparison options (default options are sufficient for a basic side‑by‑side comparison).
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the side‑by‑side comparison. The method writes the result directly to the specified output file.
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, options);

            // If you also want to ensure the output file exists via the generic save rule, you can load it and save again.
            // Document resultDoc = new Document(outputPdfPath);
            // resultDoc.Save(outputPdfPath); // complies with the document-save rule

            Console.WriteLine($"Comparison completed successfully. Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}