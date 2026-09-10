using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";   // source PDF
        const string outputPdf  = "output.pdf";  // result PDF
        const string dicomImage = "image.dcm";   // DICOM image file

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(dicomImage))
        {
            Console.Error.WriteLine($"DICOM image not found: {dicomImage}");
            return;
        }

        // Open the PDF, add the DICOM image with custom size, and save.
        using (Document doc = new Document(inputPdf))
        {
            // Desired dimensions in points (1 point = 1/72 inch).
            // Adjust these values to achieve the required resolution.
            double desiredWidth  = 200; // e.g., 200 points (~2.78 inches)
            double desiredHeight = 300; // e.g., 300 points (~4.17 inches)

            // Create an ImageStamp for the DICOM image.
            ImageStamp imgStamp = new ImageStamp(dicomImage)
            {
                // Set explicit width and height.
                Width  = desiredWidth,
                Height = desiredHeight,
                // Position the image using XIndent (left) and YIndent (top).
                // XIndent = distance from the left edge of the page.
                // YIndent = distance from the bottom edge of the page.
                XIndent = 100,
                YIndent = 500
            };

            // Add the stamp to the first page of the document.
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"DICOM image inserted and saved to '{outputPdf}'.");
    }
}
