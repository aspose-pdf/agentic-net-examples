using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // Existing PDF (can be empty)
        const string outputPdfPath  = "output_with_dicom.pdf";
        const string dicomImagePath = "image.dcm";          // DICOM image file

        // Desired output size in points (1 point = 1/72 inch)
        // Example: 200 pixels at 150 DPI => 200 / 150 * 72 = 96 points
        const double desiredWidthPoints  = 96.0; // adjust as needed
        const double desiredHeightPoints = 96.0; // adjust as needed

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

        // Load (or create) the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure at least one page exists
            Page page = pdfDoc.Pages.Count > 0 ? pdfDoc.Pages[1] : pdfDoc.Pages.Add();

            // Create an Image object and load the DICOM file from a stream
            Image dicomImg = new Image();
            using (FileStream imgStream = File.OpenRead(dicomImagePath))
            {
                dicomImg.ImageStream = imgStream;          // Assign the image data
            }

            // Apply custom dimensions (width & height) before adding to the page
            dicomImg.FixWidth  = desiredWidthPoints;   // Width in points
            dicomImg.FixHeight = desiredHeightPoints;  // Height in points

            // Enable resolution handling (optional, but respects DPI metadata)
            dicomImg.IsApplyResolution = true;

            // Position the image on the page (optional: set margins or coordinates)
            // Here we let Aspose.Pdf place it at the top‑left of the page.
            page.Paragraphs.Add(dicomImg);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with DICOM image: {outputPdfPath}");
    }
}