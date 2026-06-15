using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files to compare
        const string firstPdfPath  = "FirstDocument.pdf";
        const string secondPdfPath = "SecondDocument.pdf";

        // Folder where the diff PDF will be saved
        const string outputFolder = "DiffResults";
        // Name of the resulting diff PDF
        const string diffFileName = "ComparisonResult.pdf";

        // Validate input files
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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);
        string resultPdfPath = Path.Combine(outputFolder, diffFileName);

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create the graphical comparer
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize appearance of differences
                comparer.Color     = Aspose.Pdf.Color.Red;   // highlight color
                comparer.Threshold = 0;                      // 0% threshold (detect all changes)

                // Perform the comparison and save the diff PDF
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Diff PDF created at: {resultPdfPath}");
        }
        catch (ArgumentException ex)
        {
            // Thrown if pages have different sizes or result path is invalid
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}