using System;
using System.IO;
using System.Drawing;               // Rectangle struct required by PdfContentEditor
using Aspose.Pdf.Facades;          // Facade API for editing annotations

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

        // PdfContentEditor implements IDisposable – use using for deterministic disposal
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF document
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height)
            Rectangle rect = new Rectangle(100, 500, 200, 150);

            // Create a popup annotation:
            //   contents – text shown in the popup
            //   open     – false so it appears only on hover
            //   page     – 1‑based page index
            editor.CreatePopup(rect, "This is a hover note.", false, 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added and saved to '{outputPath}'.");
    }
}