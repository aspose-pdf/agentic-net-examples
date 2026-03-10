using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "resized.pdf";

        // Target page dimensions (points). 1 inch = 72 points.
        // Example: A4 size = 8.27 x 11.69 inches
        // PageSize constructor expects float values, so cast the calculations.
        float targetWidth  = (float)(8.27 * 72); // ≈ 595 points
        float targetHeight = (float)(11.69 * 72); // ≈ 842 points

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Use PdfPageEditor (Aspose.Pdf.Facades) to change the page size.
        // The editor works directly on the PDF file; no Document object is
        // created, so the document‑disposal rule does not apply here.
        // -----------------------------------------------------------------
        PdfPageEditor pageEditor = new PdfPageEditor();

        // Bind the source PDF. This loads the file into the facade.
        pageEditor.BindPdf(inputPath);

        // Set the desired page size. The constructor takes width and height
        // in points (float).
        pageEditor.PageSize = new PageSize(targetWidth, targetHeight);

        // Apply the changes to all pages (default behavior).
        pageEditor.ApplyChanges();

        // Save the modified PDF to the output path.
        pageEditor.Save(outputPath);

        // Release resources held by the facade.
        pageEditor.Close();

        Console.WriteLine($"PDF pages resized and saved to '{outputPath}'.");
    }
}