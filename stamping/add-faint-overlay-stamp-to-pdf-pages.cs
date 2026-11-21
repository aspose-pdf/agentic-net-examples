using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output_faint_overlay.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create an ImageStamp with the desired opacity (0.4 = 40% opaque, 60% transparent)
            ImageStamp stamp = new ImageStamp(stampImg)
            {
                Opacity = 0.4,          // Set faint overlay opacity
                Background = false      // Draw on top of page content
            };

            // Apply the stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"Faint overlay applied and saved to '{outputPdf}'.");
    }
}