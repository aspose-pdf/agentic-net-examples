using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and the source image.
        const string outputPdf = "widthBeforeFile.pdf";
        const string imagePath = "sample.jpg";

        // Verify that the image file exists before proceeding.
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a blank page to the document.
            Page page = doc.Pages.Add();

            // Create an Image object.
            Image img = new Image();

            // Set the desired fixed width (200 points) BEFORE assigning the image file.
            img.FixWidth = 200;

            // Assign the image file to the Image object.
            img.File = imagePath;

            // Add the image to the page's paragraph collection.
            page.Paragraphs.Add(img);

            // Save the PDF to the specified file.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created and saved as '{outputPdf}'.");
    }
}