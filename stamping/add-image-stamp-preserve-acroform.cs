using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";   // source PDF with AcroForm fields
        const string stampImage = "logo.png";    // image to use as stamp
        const string outputPdf  = "output.pdf";  // result PDF

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the PDF document – using ensures deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                // Position the stamp (example: bottom‑right corner)
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Make the stamp semi‑transparent
                Opacity = 0.5,
                // Provide alternative text for accessibility
                AlternativeText = "Company logo"
            };

            // Apply the stamp to every page; AcroForm fields are left unchanged
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; form fields remain intact
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}