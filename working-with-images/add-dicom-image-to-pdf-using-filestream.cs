using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the DICOM file (binary image data)
        const string dicomPath = "input.dcm";

        // Path where the resulting PDF will be saved
        const string outputPdf = "output.pdf";

        // Verify that the DICOM file exists
        if (!File.Exists(dicomPath))
        {
            Console.Error.WriteLine($"File not found: {dicomPath}");
            return;
        }

        // Open the DICOM file as a read‑only stream
        using (FileStream dicomStream = File.OpenRead(dicomPath))
        // Create a new PDF document (empty)
        using (Document pdfDoc = new Document())
        {
            // Add a single page to the document (pages are 1‑based)
            pdfDoc.Pages.Add();

            // Create an Image object and assign the DICOM stream to it
            Image dicomImage = new Image
            {
                // The ImageStream property accepts any image data stream,
                // including DICOM binary data.
                ImageStream = dicomStream
            };

            // Optionally set explicit dimensions (in points) for the image.
            // Uncomment and adjust as needed.
            // dicomImage.FixWidth = 400;
            // dicomImage.FixHeight = 400;

            // Add the image to the first page's paragraph collection.
            pdfDoc.Pages[1].Paragraphs.Add(dicomImage);

            // Save the PDF document to the specified file.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with DICOM image saved to '{outputPdf}'.");
    }
}