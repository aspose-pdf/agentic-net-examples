using System;
using System.IO;
using System.Drawing; // Needed for Rectangle used by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the existing PDF to the content editor facade.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the rectangle where the new bullet item will be placed.
            // System.Drawing.Rectangle expects (x, y, width, height).
            // Original coordinates (llx, lly, urx, ury) = (50, 500, 550, 520).
            // Width = urx - llx = 500, Height = ury - lly = 20.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(50, 500, 500, 20);

            // Text to insert: bullet character followed by the item text.
            string bulletItem = "• New bullet list item";

            // Create a simple text annotation inside the rectangle.
            // Overload: CreateText(Rectangle, string, string, bool, string, int)
            // Parameters: rectangle, text, font name, isMultiline, color name, font size.
            editor.CreateText(rect, bulletItem, "Helvetica", false, "Black", 12);

            // Save the modified PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bullet list item added. Output saved to '{outputPath}'.");
    }
}
