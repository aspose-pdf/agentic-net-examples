using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path, and image file path (used to create the memory stream)
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imageFilePath = "logo.png";

        // Verify files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Read the image into a memory stream (the stream will be disposed after stamping)
            using (FileStream imgFileStream = File.OpenRead(imageFilePath))
            using (MemoryStream imgMemoryStream = new MemoryStream())
            {
                imgFileStream.CopyTo(imgMemoryStream);
                imgMemoryStream.Position = 0; // reset to beginning

                // Create an ImageStamp from the memory stream
                ImageStamp imgStamp = new ImageStamp(imgMemoryStream)
                {
                    // Example positioning and appearance settings
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Opacity             = 0.5,          // semi‑transparent
                    Background          = false,        // stamp on top of page content
                    // Optional: set alternative text for accessibility
                    AlternativeText     = "Company logo"
                };

                // Apply the stamp to each page in the document
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(imgStamp);
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added from memory stream and saved to '{outputPdfPath}'.");
    }
}