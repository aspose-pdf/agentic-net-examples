using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // optional, kept for consistency

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // Source PDF (may contain embedded files)
        const string stampImagePath = "stamp.png";    // Image to use as stamp
        const string outputPdfPath = "output.pdf";    // Resulting PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the original PDF (embedded files are loaded automatically)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp instance – this represents the graphic stamp
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Example visual settings – adjust as required
                Background = false,          // stamp appears on top of page content
                Opacity = 0.6f,              // semi‑transparent
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                // ImageStamp does not have a Margin property; use XIndent/YIndent instead
                XIndent = 10,                // horizontal offset (margin) in points
                YIndent = 10                 // vertical offset (margin) in points
            };

            // Apply the stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // No extra work is needed to preserve embedded files:
            // they remain part of the Document object after modifications.

            // Save the modified PDF; embedded files are written out unchanged.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and PDF saved to '{outputPdfPath}'.");
    }
}
