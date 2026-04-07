using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // For additional utilities if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_collapsed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has an outline (bookmarks) collection
            if (doc.Outlines != null && doc.Outlines.Count > 0)
            {
                // Example: collapse the first top‑level outline item
                // The Open property controls the initial visibility (expanded/collapsed)
                doc.Outlines[1].Open = false;   // 1‑based indexing for outline items
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Collapsible section set to collapsed. Saved as '{outputPath}'.");
    }
}