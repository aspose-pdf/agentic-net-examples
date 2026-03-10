using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath   = "sample.png";   // image to be referenced
        const string outputPdf   = "output.pdf";

        // Ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Document pdf = new Document())
        {
            // Add a single page (pages are 1‑based)
            pdf.Pages.Add();

            // ------------------------------------------------------------
            // Add the image to the document resources only once.
            // The Add method returns the resource name; the same name can
            // be used for multiple placements, which creates only one XImage
            // object in the PDF and therefore reduces file size.
            // ------------------------------------------------------------
            string imageResourceName;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Add the image to the first page's resource collection.
                // This stores the image once and returns its internal name.
                imageResourceName = pdf.Pages[1].Resources.Images.Add(imgStream);
            }

            // ------------------------------------------------------------
            // Place the same image multiple times on the page.
            // Use the overload that accepts the resource name.
            // Each call references the already stored XImage.
            // ------------------------------------------------------------
            // First placement
            pdf.Pages[1].AddImage(imageResourceName,
                new Aspose.Pdf.Rectangle(50, 700, 250, 900));

            // Second placement (different location, same size)
            pdf.Pages[1].AddImage(imageResourceName,
                new Aspose.Pdf.Rectangle(300, 500, 500, 700));

            // Third placement (scaled differently)
            pdf.Pages[1].AddImage(imageResourceName,
                new Aspose.Pdf.Rectangle(100, 300, 200, 400));

            // Save the resulting PDF
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}