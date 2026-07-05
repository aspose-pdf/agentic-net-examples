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
        const string reportPdfPath = "highlight_report.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF containing highlight annotations
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Create a new PDF that will serve as the report
            Document reportDoc = new Document();

            // Iterate through each page of the source document
            foreach (Page srcPage in sourceDoc.Pages)
            {
                // Check each annotation on the page
                foreach (Annotation ann in srcPage.Annotations)
                {
                    // Identify highlight annotations (correct class name and case)
                    if (ann is HighlightAnnotation)
                    {
                        // Render the entire source page to a PNG image in memory using PngDevice
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            // You can adjust the resolution as needed (e.g., 150 DPI)
                            var resolution = new Resolution(150);
                            var pngDevice = new PngDevice(resolution);
                            pngDevice.Process(srcPage, imgStream);
                            imgStream.Position = 0; // Reset stream position for reading

                            // Add a new page to the report for this annotation
                            Page reportPage = reportDoc.Pages.Add();

                            // Define where the image will be placed on the report page (full page)
                            var imgRect = new Aspose.Pdf.Rectangle(
                                0, // lower‑left X
                                0, // lower‑left Y
                                reportPage.PageInfo.Width,   // upper‑right X (full page width)
                                reportPage.PageInfo.Height   // upper‑right Y (full page height)
                            );

                            // Embed the rendered image into the report page
                            reportPage.AddImage(imgStream, imgRect);
                        }
                    }
                }
            }

            // Save the generated report PDF
            reportDoc.Save(reportPdfPath);
            Console.WriteLine($"Report saved to '{reportPdfPath}'.");
        }
    }
}
