using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        // Define non‑consecutive pages (1‑based indexing) and a common zoom factor (as a percentage integer)
        int[] pagesToZoom = new int[] { 1, 3, 5 };
        double zoomFactor = 1.5; // 150 %
        int zoomPercent = (int)(zoomFactor * 100); // PdfPageEditor.Zoom expects an int (percentage)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade and bind the document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);
                // Specify the pages to be edited (non‑consecutive)
                editor.ProcessPages = pagesToZoom;
                // Apply a common zoom factor to the selected pages (integer percentage)
                editor.Zoom = zoomPercent;
                // Commit the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages {string.Join(",", pagesToZoom)} zoomed by {zoomFactor * 100}% saved to '{outputPath}'.");
    }
}
