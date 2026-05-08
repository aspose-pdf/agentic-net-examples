using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // source PDF
        const string imagePath      = "stamp.png";      // image to use as stamp
        const string outputPdfPath  = "output.pdf";     // result PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {imagePath}");
            return;
        }

        // Load the PDF document and preserve its XMP metadata
        using (Document doc = new Document(inputPdfPath))
        {
            // Extract existing XMP metadata into a memory stream
            using (MemoryStream xmpStream = new MemoryStream())
            {
                doc.GetXmpMetadata(xmpStream);   // write metadata to stream
                xmpStream.Position = 0;          // reset for later reading

                // Create an image stamp from the specified file
                Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(imagePath)
                {
                    // Example visual settings (optional)
                    Background = false,                 // stamp on top of page content
                    Opacity    = 0.5,                    // 50% transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Apply the stamp to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Re‑apply the original XMP metadata before saving
                doc.SetXmpMetadata(xmpStream);
            }

            // Save the modified PDF (using the standard Document.Save overload)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and XMP metadata retained. Output saved to '{outputPdfPath}'.");
    }
}