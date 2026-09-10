using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // Existing PDF
        const string dicomImagePath = "image.dcm";      // DICOM image file
        const string outputPdfPath  = "output.pdf";     // Result PDF

        // Verify source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(dicomImagePath))
        {
            Console.Error.WriteLine($"DICOM image not found: {dicomImagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Access the first page (1‑based indexing rule)
            Page page = pdfDoc.Pages[1];

            // Open the DICOM file as a FileStream
            using (FileStream dicomStream = new FileStream(dicomImagePath, FileMode.Open, FileAccess.Read))
            {
                // Create an Image object and assign the stream (Image constructor + ImageStream)
                Image dicomImg = new Image();
                dicomImg.ImageStream = dicomStream;

                // Define where the image will be placed on the page
                // Rectangle(left, bottom, width, height)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 200);

                // Add the image to the page at the specified rectangle
                page.AddImage(dicomImg.ImageStream, rect);
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with DICOM image saved to '{outputPdfPath}'.");
    }
}