using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string dicomPath = "image.dcm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(dicomPath))
        {
            Console.Error.WriteLine($"DICOM file not found: {dicomPath}");
            return;
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a blank page
            Page page = pdfDoc.Pages.Add();

            // Open the DICOM file as a stream and assign it to an Aspose.Pdf.Image
            using (FileStream dicomStream = File.OpenRead(dicomPath))
            {
                Image dicomImage = new Image
                {
                    ImageStream = dicomStream,
                    FixWidth = 500,   // optional width in points
                    FixHeight = 500   // optional height in points
                };

                // Add the image to the page's content
                page.Paragraphs.Add(dicomImage);

                // Save the PDF while the stream is still open
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with DICOM image saved to '{outputPdf}'.");
    }
}
