using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs to compare
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";

        // Folder where the diff PDF will be saved
        const string outputFolder = "DiffOutput";
        string diffPath = Path.Combine(outputFolder, "diff.pdf");

        // Verify input files exist
        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document sourceDoc = new Document(sourcePath))
            using (Document targetDoc = new Document(targetPath))
            {
                // Configure comparison options (default highlight color will be used)
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Perform side‑by‑side visual comparison – the method is static and writes the diff PDF directly
                SideBySidePdfComparer.Compare(sourceDoc, targetDoc, diffPath, options);
            }

            Console.WriteLine($"Diff PDF saved to '{diffPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}
