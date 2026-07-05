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
        // Name of the diff PDF file
        const string diffPdfName = "comparison_result.pdf";

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

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);
        string resultPdfPath = Path.Combine(outputFolder, diffPdfName);

        try
        {
            // Load the two documents using the recommended lifecycle pattern (using blocks)
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create an instance of GraphicalPdfComparer
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize comparer appearance (e.g., change highlight color)
                // comparer.Color = Aspose.Pdf.Color.Red; // default is red

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