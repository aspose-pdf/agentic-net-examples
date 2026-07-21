using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_custom_appearance.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the existing PDF document
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (coordinates are in points)
        // PdfContentEditor.CreateRubberStamp expects a System.Drawing.Rectangle,
        // so we convert the PDF rectangle (LLX, LLY, URX, URY) to (X, Y, Width, Height).
        System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(
            x: 100,                     // lower‑left X
            y: 500,                     // lower‑left Y (origin is bottom‑left in PDF, same for System.Drawing.Rectangle when used by Aspose)
            width: 200,                 // URX‑LLX
            height: 100                 // URY‑LLY
        );

        // Build a simple appearance stream that draws a red rectangle
        // PDF drawing operators: q (save graphics state), 1 0 0 rg (set fill color red),
        // 0 0 200 100 re (rectangle), f (fill), Q (restore graphics state)
        string appearanceCommands = "q 1 0 0 rg 0 0 200 100 re f Q";
        byte[] appearanceBytes = Encoding.ASCII.GetBytes(appearanceCommands);
        using (MemoryStream appearanceStream = new MemoryStream(appearanceBytes))
        {
            // Create a rubber stamp annotation with the custom appearance stream.
            // The overload requires System.Drawing.Color, so we use System.Drawing.Color.Blue.
            editor.CreateRubberStamp(
                page: 1,
                annotRect: annotRect,
                annotContents: "Custom Vector Graphic",
                color: System.Drawing.Color.Blue,
                appearanceStream: appearanceStream);
        }

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"PDF saved with custom annotation appearance to '{outputPath}'.");
    }
}
