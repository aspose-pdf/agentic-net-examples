using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF, output PDF and the image to be used as a stamp
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string stampImagePath = "stamp.png";

        // Ensure the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF, preserve its XMP metadata, add the image stamp, then save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ----- Preserve existing XMP metadata -----
            // Retrieve the XMP metadata into a memory stream
            using (MemoryStream xmpStream = new MemoryStream())
            {
                pdfDoc.GetXmpMetadata(xmpStream);
                // Reset the stream position so it can be read again later
                xmpStream.Position = 0;

                // ----- Add image stamp to each page -----
                foreach (Page page in pdfDoc.Pages)
                {
                    // Create a new ImageStamp for the current page
                    ImageStamp imgStamp = new ImageStamp(stampImagePath)
                    {
                        // Example visual settings (optional)
                        Opacity = 0.5,                                 // 50% transparent
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment   = VerticalAlignment.Center
                    };

                    // Place the stamp on the page
                    page.AddStamp(imgStamp);
                }

                // ----- Restore the XMP metadata -----
                // The stream still contains the original metadata; set it back onto the document
                pdfDoc.SetXmpMetadata(xmpStream);
            }

            // Save the modified PDF (using the standard Save method as required)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with image stamp and original XMP metadata: {outputPdfPath}");
    }
}