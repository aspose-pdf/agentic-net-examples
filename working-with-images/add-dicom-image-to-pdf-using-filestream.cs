using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace only

class Program
{
    static void Main()
    {
        const string outputPdfPath = "output.pdf";
        const string dicomFilePath = "image.dcm";

        // Verify that the DICOM file exists
        if (!File.Exists(dicomFilePath))
        {
            Console.Error.WriteLine($"DICOM file not found: {dicomFilePath}");
            return;
        }

        // Open the DICOM file as a FileStream
        using (FileStream dicomStream = File.OpenRead(dicomFilePath))
        // Create a new PDF document (wrapped in a using for deterministic disposal)
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document())
        {
            // Add a blank page to the document
            pdfDoc.Pages.Add();

            // Create an Image object using the default constructor
            Aspose.Pdf.Image dicomImage = new Aspose.Pdf.Image();

            // Assign the FileStream to the ImageStream property
            dicomImage.ImageStream = dicomStream;

            // Optionally, set the image size to fill the whole page
            // (PageInfo provides the page dimensions)
            Aspose.Pdf.Rectangle pageRect = new Aspose.Pdf.Rectangle(
                0,
                0,
                pdfDoc.Pages[1].PageInfo.Width,
                pdfDoc.Pages[1].PageInfo.Height);

            // Position the image using the rectangle
            dicomImage.FixWidth = pageRect.Width;
            dicomImage.FixHeight = pageRect.Height;

            // Add the image to the page's paragraph collection
            pdfDoc.Pages[1].Paragraphs.Add(dicomImage);

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with DICOM image saved to '{outputPdfPath}'.");
    }
}