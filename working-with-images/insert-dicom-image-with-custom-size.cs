using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For any text handling (not used here)

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";
        const string dicomPath = "image.dcm";

        // Verify the DICOM file exists
        if (!File.Exists(dicomPath))
        {
            Console.Error.WriteLine($"DICOM file not found: {dicomPath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document())
        {
            pdfDoc.Pages.Add();

            // Load the DICOM image from file
            using (FileStream dcmStream = File.OpenRead(dicomPath))
            {
                // Create an Image object and configure its size and resolution handling
                Aspose.Pdf.Image dicomImage = new Aspose.Pdf.Image();
                dicomImage.ImageStream = dcmStream;          // Assign the image stream
                dicomImage.IsApplyResolution = true;         // Enable resolution usage
                dicomImage.FixWidth = 300;                   // Desired width (points)
                dicomImage.FixHeight = 300;                  // Desired height (points)

                // Add the image to the first page's paragraph collection
                pdfDoc.Pages[1].Paragraphs.Add(dicomImage);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with DICOM image saved to '{outputPdf}'.");
    }
}