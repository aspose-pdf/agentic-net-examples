using System;
using System.IO;
using System.Drawing;               // Rectangle is defined here
using Aspose.Pdf.Facades;           // PdfContentEditor resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor handles the PDF document lifecycle internally.
        // Wrap it in a using block to ensure proper disposal.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF.
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height).
            // Adjust the values as needed for your document layout.
            Rectangle rect = new Rectangle(100, 500, 200, 150);

            // Multi‑line note with bullet points.
            string contents = "• First comment\r\n• Second comment\r\n• Third comment";

            // Create the popup annotation on page 1.
            // 'open' set to false so the popup is collapsed initially.
            editor.CreatePopup(rect, contents, false, 1);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added and saved to '{outputPath}'.");
    }
}