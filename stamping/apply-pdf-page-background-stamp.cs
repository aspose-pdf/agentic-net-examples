using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";   // PDF to receive the background
        const string sourcePdfPath = "background.pdf"; // PDF page used as background image
        const string outputPdfPath = "result.pdf";

        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Background file not found: {sourcePdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPdfPath))
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Render the first page of the source PDF to an image in memory.
            // The image will be used as a stamp (background) for each page of the target PDF.
            using (MemoryStream imageStream = new MemoryStream())
            {
                // You can adjust the resolution as needed (e.g., 150, 300 DPI).
                var resolution = new Resolution(150);
                var pngDevice = new PngDevice(resolution);
                pngDevice.Process(sourceDoc.Pages[1], imageStream);
                imageStream.Position = 0; // Reset stream position before using it.

                // Apply the background stamp to every page of the target document.
                foreach (Page page in targetDoc.Pages)
                {
                    // Reset the stream position for each stamp creation.
                    imageStream.Position = 0;

                    ImageStamp bgStamp = new ImageStamp(imageStream)
                    {
                        Background = true,               // Place stamp behind existing content.
                        Width = page.PageInfo.Width,    // Stretch to page width.
                        Height = page.PageInfo.Height   // Stretch to page height.
                    };

                    page.AddStamp(bgStamp);
                }
            }

            // Save the modified document.
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background applied and saved to '{outputPdfPath}'.");
    }
}
