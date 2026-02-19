using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file paths (generic names)
        string firstPdfPath = "first.pdf";
        string secondPdfPath = "second.pdf";

        // Output merged PDF path
        string outputPdfPath = "merged.pdf";

        try
        {
            // Verify that both source files exist
            if (!File.Exists(firstPdfPath))
                throw new FileNotFoundException($"Input file not found: {firstPdfPath}");

            if (!File.Exists(secondPdfPath))
                throw new FileNotFoundException($"Input file not found: {secondPdfPath}");

            // Ensure the output directory exists (handles cases where a folder is specified)
            string outputDir = Path.GetDirectoryName(outputPdfPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            // Load the two PDF documents using "using" to guarantee disposal of resources
            using (Document firstDoc = new Document(firstPdfPath))
            using (Document secondDoc = new Document(secondPdfPath))
            {
                // Append all pages from the second document to the first one
                firstDoc.Pages.Add(secondDoc.Pages);

                // Save the merged document
                firstDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
        }
        catch (FileNotFoundException ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch‑all for any unexpected runtime issues (e.g., permission problems, corrupted PDFs)
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
