using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string stampImagePath = "logo.png";

        // Verify that the source PDF and stamp image exist.
        if (!File.Exists(inputPdfPath) || !File.Exists(stampImagePath))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file.
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Align the stamp to the top‑right corner.
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;

            // Set margin offsets (e.g., 20 points from the right and top edges).
            imgStamp.RightMargin = 20;
            imgStamp.TopMargin   = 20;

            // Optional: make the stamp slightly transparent.
            imgStamp.Opacity = 0.8;

            // Apply the stamp to every page in the document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}