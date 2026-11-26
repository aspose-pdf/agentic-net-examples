using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Image

class Program
{
    static void Main()
    {
        // Input DICOM file path (could be any stream source)
        const string dicomPath = "image.dcm";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        if (!File.Exists(dicomPath))
        {
            Console.Error.WriteLine($"DICOM file not found: {dicomPath}");
            return;
        }

        // Load the DICOM image from a FileStream (any Stream works)
        using (FileStream dicomStream = File.OpenRead(dicomPath))
        {
            // Create an Image object using the parameter‑less constructor
            // Assign the stream to the Image. No ImageInfo is required – Aspose.Pdf
            // detects the format from the stream.
            Image dicomImage = new Image
            {
                ImageStream = dicomStream
            };

            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a blank page (Pages are 1‑based)
                Page page = pdfDoc.Pages.Add();

                // Add the image to the page's content
                page.Paragraphs.Add(dicomImage);

                // Save the PDF
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"DICOM image converted and saved to '{pdfPath}'.");
    }
}
