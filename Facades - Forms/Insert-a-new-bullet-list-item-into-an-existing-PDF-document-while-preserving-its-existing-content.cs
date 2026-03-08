using System;
using System.IO;
using System.Drawing; // Needed for Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // existing PDF
        const string outputPdf = "output.pdf"; // PDF with added bullet item
        const string bulletText = "\u2022 New bullet list item"; // Unicode bullet

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (a Facades class) to add a text annotation.
        // The annotation will appear as a bullet list item.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Define the rectangle where the bullet item will be placed.
            // System.Drawing.Rectangle expects (x, y, width, height).
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 600, 400, 20);

            // Create a text annotation.
            // Parameters: rectangle, text, font name, isWordWrap, color name, font size.
            editor.CreateText(rect, bulletText, "Helvetica", false, "Black", 12);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bullet list item added. Output saved to '{outputPdf}'.");
    }
}
