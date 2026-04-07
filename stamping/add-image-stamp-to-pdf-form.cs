using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "form.pdf";      // PDF that contains form fields
        const string stampImagePath = "logo.png";      // Image to be used as stamp
        const string outputPdfPath  = "form_stamped.pdf";

        // Verify that required files exist
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

        // Load the PDF document (form fields are preserved)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an image stamp – the constructor accepts a file path
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Configure stamp appearance (optional)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;   // align to right edge
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;      // align to top edge
            imgStamp.XIndent = 10;   // margin from the right edge (when Right aligned)
            imgStamp.YIndent = 10;   // margin from the top edge
            imgStamp.Opacity = 0.8;  // semi‑transparent

            // Apply the stamp to each page (or select specific pages)
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; form fields remain interactive
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
