using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath = "image.png"; // source image – can be any stream source

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the image into a memory stream – no temporary file is created for stamping.
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        using (MemoryStream imgStream = new MemoryStream(imageBytes))
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF.
            fileStamp.BindPdf(inputPdfPath);

            // Create a Stamp object and bind the image from the memory stream.
            Stamp stamp = new Stamp();
            stamp.BindImage(imgStream); // the stream must stay open until stamping is finished

            // Configure stamp appearance.
            stamp.SetOrigin(100, 500);          // Position (X, Y) from bottom‑left.
            stamp.SetImageSize(150, 100);       // Width and height.
            stamp.Opacity = 0.5f;               // Semi‑transparent.
            stamp.IsBackground = false;        // Place on top of page content.

            // Add the stamp to all pages of the document.
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF.
            fileStamp.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
    }
}
