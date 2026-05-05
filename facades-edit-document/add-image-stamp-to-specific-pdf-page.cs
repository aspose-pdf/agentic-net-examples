using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stampImage.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the facade with input and output files
        using (PdfFileStamp fileStamp = new PdfFileStamp(inputPdf, outputPdf))
        {
            // Create a stamp based on an image
            Stamp stamp = new Stamp();
            stamp.BindImage(imagePath);               // Set the image to be used as stamp
            stamp.SetOrigin(100, 500);                // Position (X,Y) from lower‑left corner
            stamp.SetImageSize(150, 100);             // Width and height of the stamp
            stamp.Opacity = 0.8f;                     // Semi‑transparent
            stamp.IsBackground = false;               // Place on top of page content

            // Apply the stamp only to the second page (pages are 1‑based)
            stamp.Pages = new int[] { 2 };

            // Add the configured stamp to the document
            fileStamp.AddStamp(stamp);

            // Close the facade to write the output file
            fileStamp.Close();
        }

        Console.WriteLine($"Image stamp added to page 2 and saved as '{outputPdf}'.");
    }
}