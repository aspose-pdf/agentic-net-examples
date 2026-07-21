using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Core Aspose.Pdf devices namespace (no Facades)

class Program
{
    static void Main()
    {
        // Input PDF file and output directory for the images
        const string inputPdfPath = "input.pdf";
        const string outputImgDir = "PageImages";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputImgDir);

        try
        {
            // Load the PDF document using the core Document class
            Document pdfDocument = new Document(inputPdfPath);

            // Iterate through each page and convert it to an image (JPEG in this example)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Define the image conversion device – you can change the format/resolution as needed
                // Here we use JPEG with 150 DPI resolution
                var jpegDevice = new JpegDevice(new Resolution(150));

                // Prepare the output file path
                string outputFilePath = Path.Combine(outputImgDir, $"Page_{pageNumber}.jpg");

                // Convert the specific page to an image and write directly to the file
                using (FileStream imageStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
            }

            Console.WriteLine($"All pages have been converted to images in folder: {outputImgDir}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
