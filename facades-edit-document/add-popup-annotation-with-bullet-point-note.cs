using System;
using System.IO;
using System.Drawing;               // Rectangle is defined here
using Aspose.Pdf.Facades;          // PdfContentEditor resides in this namespace

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

        // Bind the existing PDF and add a popup annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Position and size of the popup annotation (x, y, width, height)
            Rectangle rect = new Rectangle(100, 500, 300, 200);

            // Multi‑line note with bullet points
            string contents =
                "• First comment\r\n" +
                "• Second comment\r\n" +
                "• Additional details:\r\n" +
                "   – Sub‑point A\r\n" +
                "   – Sub‑point B";

            // Create the popup; set 'open' to false so it appears collapsed initially
            editor.CreatePopup(rect, contents, false, 1);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added and saved to '{outputPath}'.");
    }
}