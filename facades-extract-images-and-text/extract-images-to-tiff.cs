using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedTiffImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            // Initialize the extractor and bind the PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);
                extractor.ExtractImage();

                int imageIndex = 1;

                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    // Retrieve the current image into a memory stream
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset for reading

                        // Create a temporary PDF document containing the image
                        using (Document tempDoc = new Document())
                        {
                            tempDoc.Pages.Add();

                            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                            {
                                ImageStream = imageStream
                            };

                            tempDoc.Pages[1].Paragraphs.Add(pdfImage);

                            // Configure TIFF conversion settings (lossless)
                            TiffSettings tiffSettings = new TiffSettings
                            {
                                Compression = CompressionType.None,
                                Depth = ColorDepth.Default,
                                Shape = ShapeType.Landscape,
                                SkipBlankPages = false
                            };

                            // Define resolution (e.g., 300 DPI)
                            Resolution resolution = new Resolution(300);

                            // Create the TIFF device with the above settings
                            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

                            // Output file path for the TIFF image
                            string tiffOutputPath = Path.Combine(outputFolder, $"image_{imageIndex}.tiff");

                            // Convert the temporary PDF (containing the image) to a single-page TIFF
                            tiffDevice.Process(tempDoc, tiffOutputPath);
                        }
                    }

                    Console.WriteLine($"Extracted and saved TIFF #{imageIndex}");
                    imageIndex++;
                }
            }

            Console.WriteLine("Image extraction and TIFF conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}