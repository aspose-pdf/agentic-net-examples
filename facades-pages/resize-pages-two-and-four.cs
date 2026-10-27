using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to scale page contents (ResizeContents is not available in recent versions)
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Specify the pages to affect (1‑based indexing)
        editor.ProcessPages = new int[] { 2, 4 };

        // Apply a uniform zoom factor. Adjust the factor to achieve the desired left/right margins.
        editor.Zoom = 0.9f; // 90% of original size (example)

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Pages 2 and 4 resized and saved to '{outputPath}'.");
    }
}
