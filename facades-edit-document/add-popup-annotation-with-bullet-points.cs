using System;
using System.IO;
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

        // Multi‑line note with bullet points
        string popupContent = "• First point\r\n• Second point\r\n• Additional details:\r\n   - Subitem A\r\n   - Subitem B";

        // Rectangle defining the popup location (System.Drawing.Rectangle is required by the facade)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 150);

        // Bind the PDF and create the popup annotation on page 1 (open = false)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        editor.CreatePopup(rect, popupContent, false, 1);

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Popup annotation added and saved to '{outputPath}'.");
    }
}