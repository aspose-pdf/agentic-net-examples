using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades; // PdfExtractor lives here

class Program
{
    // Simulated Azure Blob container – a local folder
    private const string ContainerPath = "pdf-images";

    static async Task Main(string[] args)
    {
        // Input PDF path
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the simulated container folder exists
        Directory.CreateDirectory(ContainerPath);

        // Use PdfExtractor (IDisposable) to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to pull images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0; // reset for saving

                    // Determine a file name (e.g., image_1.png, image_2.png, etc.)
                    // The extractor does not provide the original format, so we use .png as a safe default.
                    string fileName = $"image_{imageIndex}.png";
                    string filePath = Path.Combine(ContainerPath, fileName);

                    // Save the image to the simulated container folder
                    using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        imageStream.CopyTo(file);
                    }

                    Console.WriteLine($"Saved {fileName} to '{ContainerPath}'.");
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
        await Task.CompletedTask; // keep async signature
    }
}
