using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string dicomPath = "image.dcm";   // Path to the DICOM image
        const string outputPdf = "output.pdf";  // Resulting PDF file

        // Verify the DICOM file exists
        if (!File.Exists(dicomPath))
        {
            Console.Error.WriteLine($"DICOM file not found: {dicomPath}");
            return;
        }

        // Create a new PDF document
        Document pdfDoc = new Document();
        // Add a blank page to host the image
        Page page = pdfDoc.Pages.Add();

        // Load the DICOM image from a stream and add it as an ImageStamp
        using (FileStream imgStream = File.OpenRead(dicomPath))
        {
            ImageStamp stamp = new ImageStamp(imgStream)
            {
                // Set custom size (points; 1 point = 1/72 inch)
                Width = 300f,   // ≈4.17 inches
                Height = 400f,  // ≈5.56 inches
                // Optional: center the image on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };
            page.AddStamp(stamp);
        }

        // Save the resulting PDF
        pdfDoc.Save(outputPdf);
        Console.WriteLine($"PDF with DICOM image saved to '{outputPdf}'.");
    }
}
