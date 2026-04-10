using System;
using System.IO;
using System.Drawing;               // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // PdfContentEditor resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, add a popup annotation, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF.
            editor.BindPdf(inputPath);

            // Define the rectangle where the popup icon will appear.
            // (x, y, width, height) – coordinates are in points.
            // CreatePopup expects a System.Drawing.Rectangle, not Aspose.Pdf.Rectangle.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 700, 30, 30);

            // Multi‑line note with bullet points.
            string contents =
                "• First comment\r\n" +
                "• Second comment\r\n" +
                "• Additional details:\r\n" +
                "   - Sub point A\r\n" +
                "   - Sub point B";

            // Create the popup annotation on page 1.
            // The 'open' flag is set to false so the popup is closed initially.
            editor.CreatePopup(rect, contents, false, 1);

            // Persist the changes.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added and saved to '{outputPath}'.");
    }
}
