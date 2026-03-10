using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "output.pdf";     // PDF with adjusted image DPI
        const int    targetDpi  = 150;              // desired DPI for embedded images

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Create an empty PDF that will hold the re‑rendered pages
            using (Document outDoc = new Document())
            {
                // Prepare a device that renders pages to JPEG at the target DPI
                Resolution resolution = new Resolution(targetDpi);
                JpegDevice jpegDevice = new JpegDevice(resolution);

                // Process each page
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    Page srcPage = srcDoc.Pages[i];

                    // Render the page to an in‑memory JPEG image
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        jpegDevice.Process(srcPage, imgStream);
                        imgStream.Position = 0; // reset for reading

                        // Create a new page in the output PDF
                        Page outPage = outDoc.Pages.Add();

                        // Set the page size to match the rendered image dimensions
                        outPage.PageInfo.Width  = jpegDevice.Width;
                        outPage.PageInfo.Height = jpegDevice.Height;

                        // Add the JPEG image to the new page, covering the whole page area
                        outPage.AddImage(
                            imgStream,
                            new Aspose.Pdf.Rectangle(0, 0, jpegDevice.Width, jpegDevice.Height));
                    }
                }

                // Save the resulting PDF
                outDoc.Save(outputPath);
                Console.WriteLine($"PDF saved with images at {targetDpi} DPI: {outputPath}");
            }
        }
    }
}