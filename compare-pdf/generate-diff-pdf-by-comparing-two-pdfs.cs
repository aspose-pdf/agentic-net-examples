using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Folder where the diff PDF will be saved
        const string outputFolder = "DiffResults";

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Path for the resulting diff PDF
        string diffPdfPath = Path.Combine(outputFolder, "diff.pdf");

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

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create the comparer instance
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Perform the comparison and save the diff PDF
                comparer.CompareDocumentsToPdf(doc1, doc2, diffPdfPath);
            }

            Console.WriteLine($"Diff PDF created at: {diffPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}