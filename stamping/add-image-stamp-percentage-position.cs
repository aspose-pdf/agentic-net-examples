using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "logo.png";

        // Desired offsets as percentages of page dimensions
        const double percentFromLeft   = 0.10; // 10% of page width
        const double percentFromBottom = 0.20; // 20% of page height

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the image file
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Apply the stamp to each page with percentage‑based positioning
            foreach (Page page in doc.Pages)
            {
                // Page size in points (1 point = 1/72 inch)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Compute absolute offsets from percentages
                imgStamp.XIndent = pageWidth  * percentFromLeft;
                imgStamp.YIndent = pageHeight * percentFromBottom;

                // Optional: set explicit size for the stamp
                // imgStamp.Width  = 100; // points
                // imgStamp.Height = 50;  // points

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}