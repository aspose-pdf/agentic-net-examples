using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Annotations;        // For annotation types (if needed)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with existing annotations
        const string stampImg  = "stamp.png";          // image to use as stamp
        const string outputPdf = "output_with_stamp.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position the stamp – you can adjust these margins as needed
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Example: make the stamp semi‑transparent
                Opacity = 0.5f,
                // Ensure the stamp is drawn on top of existing content (default)
                Background = false
            };

            // Add the stamp to the first page (preserves existing annotations)
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}