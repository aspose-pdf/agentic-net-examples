using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Convert only the first five pages (or fewer if the document has less)
            int pageCount = Math.Min(5, pdfDoc.Pages.Count);
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Create a JPEG device with the required resolution (200 DPI)
                var jpegDevice = new JpegDevice(new Resolution(200));

                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.jpg");
                // Save the page as a JPEG image
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
