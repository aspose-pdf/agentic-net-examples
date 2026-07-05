using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the image will be placed.
            // Parameters: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 650);

            // Insert the raster image at the specified coordinates
            page.AddImage(imagePath, rect);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image inserted and saved to '{outputPdf}'.");
    }
}