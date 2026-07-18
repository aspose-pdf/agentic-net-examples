using System;
using System.IO;
using System.Drawing;               // Rectangle is defined here
using Aspose.Pdf.Facades;          // PdfContentEditor facade

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

        // Multi‑line note with bullet points
        string popupContents = "• First comment\r\n• Second comment\r\n• Additional details";

        // Annotation rectangle (x, y, width, height) – coordinates are in points
        Rectangle rect = new Rectangle(100, 500, 200, 150);

        // Use PdfContentEditor to add the popup annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);                 // Load the PDF
            editor.CreatePopup(rect, popupContents, false, 1); // page numbers are 1‑based
            editor.Save(outputPath);                   // Save the modified PDF
        }

        Console.WriteLine($"Popup annotation saved to '{outputPath}'.");
    }
}