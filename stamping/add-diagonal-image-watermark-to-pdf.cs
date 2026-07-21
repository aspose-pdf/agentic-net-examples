using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imageFile = "watermark.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine($"Image file not found: {imageFile}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the image file
            ImageStamp stamp = new ImageStamp(imageFile);

            // Rotate 90 degrees to make the watermark diagonal
            stamp.Rotate = Rotation.on90; // correct enum value

            // Optional visual settings
            stamp.Opacity = 0.3;                     // semi‑transparent
            stamp.Background = false;                // draw on top of page content
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Diagonal watermark saved to '{outputPdf}'.");
    }
}
