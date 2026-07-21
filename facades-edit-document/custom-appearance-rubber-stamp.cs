using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – replace with actual file locations
        const string inputPdfPath      = "input.pdf";
        const string outputPdfPath     = "output_with_custom_stamp.pdf";
        const string appearancePdfPath = "custom_appearance.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(appearancePdfPath))
        {
            Console.Error.WriteLine($"Appearance PDF not found: {appearancePdfPath}");
            return;
        }

        // Create the content editor facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdfPath);

            // Define the annotation rectangle (x, y, width, height) using System.Drawing.Rectangle
            // Note: PdfContentEditor expects System.Drawing.Rectangle, so we fully qualify it.
            var annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Open the custom appearance PDF as a stream
            using (FileStream appearanceStream = File.OpenRead(appearancePdfPath))
            {
                // Create a rubber stamp annotation on page 1 with custom appearance
                // Parameters: page number (1‑based), rectangle, contents text, color, appearance stream
                editor.CreateRubberStamp(
                    page: 1,
                    annotRect: annotRect,
                    annotContents: "Custom Stamp",
                    color: System.Drawing.Color.Blue,
                    appearanceStream: appearanceStream);
            }

            // Save the modified document
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with custom appearance stamp: {outputPdfPath}");
    }
}