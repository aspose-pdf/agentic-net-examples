using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF to obtain page dimensions (if needed for calculations)
        using (Document srcDoc = new Document(inputPath))
        {
            // Initialize FormEditor with source and destination files
            using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
            {
                // Define margins in points (1 inch = 72 points)
                float leftMargin   = 50f; // distance from the left edge
                float bottomMargin = 20f; // distance from the bottom edge

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
                {
                    // Create a unique field name for each page to avoid name collisions
                    string newFieldName = $"FooterNote_{pageNum}";

                    // Copy the existing field "FooterNote" to the current page at the calculated coordinates
                    // Parameters: source field name, new field name, target page number, X (abscissa), Y (ordinate)
                    formEditor.CopyInnerField("FooterNote", newFieldName, pageNum, leftMargin, bottomMargin);
                }

                // Persist the changes to the output file
                formEditor.Save();
            }
        }

        Console.WriteLine($"Field \"FooterNote\" moved to bottom margin and saved as '{outputPath}'.");
    }
}