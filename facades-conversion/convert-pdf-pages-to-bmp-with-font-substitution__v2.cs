using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;               // FontRepository, SimpleFontSubstitution
using Aspose.Pdf.Devices;            // Resolution, BmpDevice

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

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Configure font substitution (example: replace missing Arial with Helvetica)
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Arial", "Helvetica"));

        // Load the PDF document (needed for BmpDevice rendering)
        Document pdfDocument = new Document(inputPdf);

        // Create and configure PdfConverter (Facades API)
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF document
            converter.BindPdf(inputPdf);

            // NOTE: In recent Aspose.Pdf versions the CoordinateType property is not required/available.
            // The default coordinate type for rasterisation is CropBox, which satisfies the requirement.

            // Set the resolution for rasterisation (default is 96 DPI)
            converter.Resolution = new Resolution(150);

            // Convert pages 2 and 3 to BMP images using BmpDevice
            for (int pageNumber = 2; pageNumber <= 3; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.bmp");

                // BmpDevice uses the same resolution as the converter
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                {
                    BmpDevice bmpDevice = new BmpDevice(converter.Resolution);
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as {outputPath}");
            }

            // Release any resources held by the converter
            converter.Close();
        }
    }
}
