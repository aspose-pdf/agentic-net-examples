using System;
using System.IO;
using System.Drawing;               // for System.Drawing.Rectangle and Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create and bind the PDF using PdfContentEditor (facade API)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (position and size) using System.Drawing.Rectangle
            // x, y = lower‑left corner in PDF coordinates, width = URX‑LLX, height = URY‑LLY
            System.Drawing.Rectangle popupRect = new System.Drawing.Rectangle(100, 500, 200, 300);

            // Multi‑line note with bullet points
            string popupContents =
                "• First comment line\r\n" +
                "• Second comment line\r\n" +
                "• Third comment line";

            // Create the popup annotation on page 1, initially open
            editor.CreatePopup(popupRect, popupContents, true, 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added. Saved to '{outputPath}'.");
    }
}
