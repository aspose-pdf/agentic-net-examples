using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string reportPdfPath = "report.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the source PDF containing highlight annotations
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Create a new PDF that will serve as the report
            using (Document reportDoc = new Document())
            {
                // Add a single page to the report where images will be placed
                Page reportPage = reportDoc.Pages.Add();

                // Positioning variables for placing images on the report page
                double startX = 50;
                double startY = 750;
                double imageWidth = 200;
                double imageHeight = 200;
                double verticalSpacing = 20;

                // Iterate through all pages of the source document
                foreach (Page srcPage in sourceDoc.Pages)
                {
                    // Iterate through all annotations on the current page
                    foreach (Annotation ann in srcPage.Annotations)
                    {
                        // Process only Highlight annotations
                        if (ann is HighlightAnnotation)
                        {
                            // Render the entire source page to an image (PNG)
                            using (MemoryStream imageStream = new MemoryStream())
                            {
                                // PngDevice renders a page to a PNG image
                                PngDevice pngDevice = new PngDevice(new Resolution(150));
                                pngDevice.Process(srcPage, imageStream);
                                imageStream.Position = 0; // Reset stream for reading

                                // Define the rectangle where the image will be placed on the report page
                                Aspose.Pdf.Rectangle imgRect = new Aspose.Pdf.Rectangle(
                                    startX,
                                    startY - imageHeight,
                                    startX + imageWidth,
                                    startY
                                );

                                // Add the image to the report page
                                reportPage.AddImage(imageStream, imgRect);

                                // Update Y position for the next image
                                startY -= (imageHeight + verticalSpacing);
                            }
                        }
                    }
                }

                // Save the generated report PDF
                reportDoc.Save(reportPdfPath);
                Console.WriteLine($"Report generated: {reportPdfPath}");
            }
        }
    }
}