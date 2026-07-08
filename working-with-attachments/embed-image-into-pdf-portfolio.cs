using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "portfolio.pdf";
        const string imagePath = "image.png";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document and enable portfolio (collection) mode
        using (Document doc = new Document())
        {
            doc.Collection = new Collection();   // marks the PDF as a portfolio
            doc.Pages.Add();                     // optional blank page

            // Embed the image file and set its display name in the portfolio
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Create a file specification from the image stream
                FileSpecification fileSpec = new FileSpecification(imgStream, Path.GetFileName(imagePath));

                // The key used in the EmbeddedFiles collection becomes the display name
                string displayName = "My Embedded Image.png";

                // Add the file specification to the portfolio
                doc.EmbeddedFiles.Add(displayName, fileSpec);
            }

            // Save the PDF portfolio
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Portfolio saved to '{outputPdf}'.");
    }
}