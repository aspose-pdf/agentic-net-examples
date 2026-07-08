using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputImg = "page1.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a stream for the output image
            using (FileStream imgStream = new FileStream(outputImg, FileMode.Create, FileAccess.Write))
            {
                // Create a PNG device with the desired resolution (e.g., 300 DPI)
                // ImageDevice is abstract; use a concrete implementation such as PngDevice.
                PngDevice pngDevice = new PngDevice(new Resolution(300));

                // Configure rendering options to improve anti‑aliasing quality
                RenderingOptions renderOpts = new RenderingOptions
                {
                    // Enable high‑quality interpolation for smoother lines and curves
                    InterpolationHighQuality = true
                };

                // Assign the custom rendering options to the device
                pngDevice.RenderingOptions = renderOpts;

                // Render the first page of the PDF to the image stream using the configured device
                pngDevice.Process(pdfDoc.Pages[1], imgStream);
            }
        }

        Console.WriteLine($"Page rendered with enhanced anti‑aliasing saved to '{outputImg}'.");
    }
}
