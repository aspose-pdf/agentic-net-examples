using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files to compare
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Folder where the diff PDF will be saved
        const string outputFolder = "DiffResults";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);
        string resultPdfPath = Path.Combine(outputFolder, "diff.pdf");

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create the graphical comparer and generate the diff PDF
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Diff PDF created at: {resultPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}