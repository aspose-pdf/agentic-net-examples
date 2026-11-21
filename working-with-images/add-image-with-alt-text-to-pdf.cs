using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for ImageStamp (inherits from StampAnnotation)

class Program
{
    static void Main()
    {
        const string imagePath = "photo.jpg";   // image to add
        const string outputPdf = "output.pdf"; // result PDF

        // Ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create an ImageStamp directly from the image file (or stream)
            ImageStamp stamp = new ImageStamp(imagePath)
            {
                XIndent = 100,                     // horizontal position
                YIndent = 500,                     // vertical position
                // Assign alternative (alt) text for assistive technologies
                AlternativeText = "A scenic mountain view with a lake in the foreground."
            };

            // Add the stamp to the page
            page.AddStamp(stamp);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image and alt text saved to '{outputPdf}'.");
    }
}
