using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";      // source PDF with existing bookmarks/outlines
        const string stampImage = "logo.png";    // image to be used as stamp
        const string outputPdf = "output_with_stamp.pdf";

        // Verify that required files exist
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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                // Positioning – center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Optional visual settings
                Opacity   = 0.5f,   // semi‑transparent
                Background = false   // stamp appears on top of page content
                // Width/Height can be set to scale the image if needed
                // Width  = 100,
                // Height = 50
            };

            // Apply the same stamp to every page.
            // Page.AddStamp does not modify bookmarks or the document outline.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified document. The original bookmarks/outlines are preserved.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added successfully. Output saved to '{outputPdf}'.");
    }
}
