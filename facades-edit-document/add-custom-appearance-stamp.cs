using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Rectangle and System.Drawing.Color used by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";            // Source PDF
        const string outputPdf  = "output_with_custom_stamp.pdf";
        const string appearancePdf = "custom_appearance.pdf"; // PDF containing the graphic to use as appearance

        // Ensure files exist
        if (!File.Exists(inputPdf) || !File.Exists(appearancePdf))
        {
            Console.Error.WriteLine("Required file(s) not found.");
            return;
        }

        // Create a PdfContentEditor facade, bind the source PDF, add a rubber‑stamp annotation
        // with a custom appearance stream (provided as a PDF file stream), then save.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Define the rectangle where the stamp will be placed (coordinates are in points)
        // PdfContentEditor.CreateRubberStamp expects a System.Drawing.Rectangle, so we use that type.
        System.Drawing.Rectangle stampRect = new System.Drawing.Rectangle(100, 500, 200, 150); // x, y, width, height

        // Open the appearance PDF as a stream and use it for the annotation
        using (FileStream appStream = File.OpenRead(appearancePdf))
        {
            // CreateRubberStamp(page, rect, contents, color, appearanceStream)
            // Page numbers are 1‑based.
            editor.CreateRubberStamp(
                page: 1,
                annotRect: stampRect,
                annotContents: "Custom Graphic Stamp",
                color: System.Drawing.Color.Black,
                appearanceStream: appStream);
        }

        // Save the modified document
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"PDF saved with custom appearance stamp: {outputPdf}");
    }
}
