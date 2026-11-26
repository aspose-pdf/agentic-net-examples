using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "portrait.png";
        const string outputPath = "height200.pdf";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (Aspose.Pdf uses 1‑based page indexing)
            Page page = doc.Pages.Add();

            // Instantiate an Image, set its fixed height, and assign the source file
            Aspose.Pdf.Image img = new Aspose.Pdf.Image
            {
                FixHeight = 200,   // Height in points
                File = imagePath
            };

            // Add the image to the page's content
            page.Paragraphs.Add(img);

            // Save the document as a PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}