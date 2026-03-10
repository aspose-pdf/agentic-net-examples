using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class InsertBulletItem
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // existing PDF
        const string outputPdf = "output.pdf";     // PDF with new bullet item

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfContentEditor is a Facade class that can add text annotations.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPdf);

            // Define the rectangle where the new bullet item will appear.
            // PdfContentEditor.CreateText expects a System.Drawing.Rectangle, not Aspose.Pdf.Rectangle.
            // Convert the coordinates (llx, lly, urx, ury) to (x, y, width, height).
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                x: 50,                                 // left (llx)
                y: 700,                                // bottom (lly)
                width: 550 - 50,                       // urx - llx
                height: 720 - 700                      // ury - lly
            );

            // Text to insert – a bullet character followed by the item text.
            string bulletText = "• New bullet list item";

            // Create a text annotation (visible) at the specified rectangle.
            // Parameters: rectangle, text, title, isVisible, author, flags
            // Flags = 0 (default) – no special annotation flags.
            editor.CreateText(rect, bulletText, "BulletItem", true, "AsposeDemo", 0);

            // Save the modified PDF. Save without explicit SaveOptions writes PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bullet item inserted and saved to '{outputPdf}'.");
    }
}
