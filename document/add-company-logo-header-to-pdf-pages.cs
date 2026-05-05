using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // existing PDF
        const string logoImage = "logo.png";       // company logo image
        const string outputPdf = "output_with_header.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoImage))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImage}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages and add the logo as a header stamp
            foreach (Page page in doc.Pages)
            {
                // Create an image stamp from the logo file
                ImageStamp imgStamp = new ImageStamp(logoImage);

                // Position the stamp at the top center of the page
                imgStamp.Background = false;                         // draw on top of page content
                imgStamp.Opacity = 1.0f;                             // fully opaque
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment   = VerticalAlignment.Top;

                // Optional: add a small top margin so the logo is not flush with the edge
                imgStamp.TopMargin = 10; // points from the top edge

                // Apply the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header with logo added to all pages. Saved as '{outputPdf}'.");
    }
}