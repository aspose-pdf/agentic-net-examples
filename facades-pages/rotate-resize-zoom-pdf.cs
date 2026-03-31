using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Example input and output file names (must exist in the working directory)
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";
        // Desired transformation parameters
        int rotation = 90;               // 0, 90, 180 or 270 degrees
        float pageWidth = 595f;          // A4 width in points (1 inch = 72 points)
        float pageHeight = 842f;         // A4 height in points
        int zoom = 150;                  // 150% zoom (PdfPageEditor.Zoom expects an int representing percentage)

        ApplyTransformations(inputPath, outputPath, rotation, pageWidth, pageHeight, zoom);
        Console.WriteLine("PDF transformation completed.");
    }

    public static void ApplyTransformations(string inputPath, string outputPath, int rotation, float pageWidth, float pageHeight, int zoom)
    {
        // Ensure the source file exists before processing
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable, so use a using block
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // Set rotation (must be 0, 90, 180 or 270)
            editor.Rotation = rotation;

            // Set the output page size (constructor expects float values)
            editor.PageSize = new PageSize(pageWidth, pageHeight);

            // Set zoom factor (integer percentage, 100 = 100%)
            editor.Zoom = zoom;

            // Apply the configured changes to the document
            editor.ApplyChanges();

            // Save the modified PDF to the specified output path
            editor.Save(outputPath);
        }
    }
}
