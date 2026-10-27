using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for converting a PDF page to an image

class Program
{
    static void Main()
    {
        const string templatePdfPath = "template.pdf";   // PDF containing the page to use as a stamp
        const string sourcePdfPath   = "source.pdf";     // PDF to which the stamp will be applied
        const string outputPdfPath   = "stamped_output.pdf";

        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the PDF that provides the stamp page (template)
        using (Document templateDoc = new Document(templatePdfPath))
        // Load the PDF that will receive the stamp
        using (Document targetDoc = new Document(sourcePdfPath))
        {
            // Convert the first page of the template PDF to an image (PNG) in memory.
            // This image will be used as a stamp for the target document.
            ImageStamp pageStamp;
            using (var imageStream = new MemoryStream())
            {
                // 300 DPI gives a good quality image; adjust as needed.
                var resolution = new Resolution(300);
                var pngDevice = new PngDevice(resolution);
                pngDevice.Process(templateDoc.Pages[1], imageStream);
                imageStream.Position = 0; // rewind the stream for reading
                pageStamp = new ImageStamp(imageStream);
                pageStamp.Background = true; // place the stamp behind existing content
                // Optionally set the stamp dimensions/position here, e.g.:
                // pageStamp.Width = targetDoc.Pages[1].PageInfo.Width;
                // pageStamp.Height = targetDoc.Pages[1].PageInfo.Height;
            }

            // Apply the image‑based stamp to every page of the target PDF.
            for (int i = 1; i <= targetDoc.Pages.Count; i++)
            {
                targetDoc.Pages[i].AddStamp(pageStamp);
            }

            // Save the modified PDF
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
