using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // existing PDF
        const string imageFilePath = "image.png";   // image to insert
        const string outputPdfPath = "output.pdf";  // result PDF

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imageFilePath))
        {
            Console.Error.WriteLine($"Image file not found: {imageFilePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure there is at least one page (page indexing is 1‑based)
            if (pdfDoc.Pages.Count == 0)
                pdfDoc.Pages.Add();

            // Create an Image object and set its source file
            Image img = new Image();
            img.File = imageFilePath;

            // Optional: set image dimensions or alignment
            // img.FixWidth = 200;
            // img.FixHeight = 150;
            // img.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the image to the first page's paragraph collection
            pdfDoc.Pages[1].Paragraphs.Add(img);

            // Save the modified PDF (lifecycle rule: save inside using block)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image added and saved to '{outputPdfPath}'.");
    }
}