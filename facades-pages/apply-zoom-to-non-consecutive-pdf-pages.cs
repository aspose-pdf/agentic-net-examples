using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "zoomed_output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Define the pages to process (non‑consecutive, 1‑based indexing)
        // Example: pages 2, 5 and 8
        int[] pagesToZoom = new int[] { 2, 5, 8 };

        // Desired zoom factor (e.g., 150% = 1.5f)
        float zoomFactor = 1.5f;

        // Use PdfPageEditor from Aspose.Pdf.Facades to apply the zoom
        using (Aspose.Pdf.Facades.PdfPageEditor editor = new Aspose.Pdf.Facades.PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Specify which pages to process
            editor.ProcessPages = pagesToZoom;

            // Apply the common zoom factor to the selected pages
            editor.Zoom = zoomFactor;

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied to pages {string.Join(", ", pagesToZoom)}. Output saved to '{outputPath}'.");
    }
}