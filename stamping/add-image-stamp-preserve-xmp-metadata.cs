using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string outputPdfPath = "output.pdf";     // destination PDF
        const string stampImagePath = "stamp.png";     // image to use as stamp

        // Ensure the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the stamp image exists
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF, retain its XMP metadata, add the image stamp, then save.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Preserve existing XMP metadata.
            // -----------------------------------------------------------------
            // Retrieve the XMP metadata into a memory stream.
            using (MemoryStream xmpStream = new MemoryStream())
            {
                pdfDoc.GetXmpMetadata(xmpStream);
                // Reset the stream position so it can be read again later.
                xmpStream.Position = 0;

                // -----------------------------------------------------------------
                // 2. Create an ImageStamp.
                // -----------------------------------------------------------------
                ImageStamp imgStamp = new ImageStamp(stampImagePath)
                {
                    // Example visual settings – adjust as needed.
                    Opacity = 0.5,                                 // 50% transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // -----------------------------------------------------------------
                // 3. Apply the stamp to every page.
                // -----------------------------------------------------------------
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // -----------------------------------------------------------------
                // 4. Restore the original XMP metadata.
                // -----------------------------------------------------------------
                // Ensure the stream is positioned at the beginning before setting.
                xmpStream.Position = 0;
                pdfDoc.SetXmpMetadata(xmpStream);
            }

            // -----------------------------------------------------------------
            // 5. Save the modified PDF.
            // -----------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and XMP metadata retained. Output saved to '{outputPdfPath}'.");
    }
}