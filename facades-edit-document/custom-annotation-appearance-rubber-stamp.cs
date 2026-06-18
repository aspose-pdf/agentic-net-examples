using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_custom_appearance.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Create a memory stream that contains a simple PDF appearance stream.
        // This stream draws a filled red rectangle (100x50 points) using PDF drawing operators.
        // The content is: q (save graphics state) 1 0 0 rg (set fill color to red) 0 0 100 50 re (rectangle) f (fill) Q (restore graphics state)
        MemoryStream appearanceStream = new MemoryStream();
        using (StreamWriter writer = new StreamWriter(appearanceStream, System.Text.Encoding.ASCII, 1024, true))
        {
            writer.WriteLine("q");
            writer.WriteLine("1 0 0 rg");
            writer.WriteLine("0 0 100 50 re");
            writer.WriteLine("f");
            writer.WriteLine("Q");
            writer.Flush();
        }
        // Reset the stream position so Aspose.Pdf can read from the beginning
        appearanceStream.Position = 0;

        // Define the annotation rectangle (x, y, width, height) in points.
        // System.Drawing.Rectangle is required by PdfContentEditor.
        Rectangle annotRect = new Rectangle(100, 500, 200, 100);

        // Create and configure the PdfContentEditor facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Create a rubber stamp annotation on page 1 with a custom appearance stream.
            // Parameters: page number, annotation rectangle, contents, color, appearance stream.
            editor.CreateRubberStamp(
                page: 1,
                annotRect: annotRect,
                annotContents: "Custom Vector Graphic Stamp",
                color: Color.Red,
                appearanceStream: appearanceStream);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom annotation appearance: {outputPdf}");
    }
}