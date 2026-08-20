using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string stampImagePath = "stamp.png";  // image to use as stamp
        const string outputPdfPath = "output.pdf";  // result PDF

        // Verify required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(stampImagePath))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Set the stamp to be drawn as background (behind page content)
            imgStamp.Background = true;

            // Optional: position and appearance settings
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;
            imgStamp.Opacity = 0.5f; // semi‑transparent background

            // Apply the stamp to each page (Page.AddStamp expects a Stamp)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}