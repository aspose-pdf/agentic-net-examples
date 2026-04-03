using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "template.pdf";   // existing PDF to which the image will be added
        const string outputPdf  = "result.pdf";
        const string dicomImage = "image.dcm";      // path to the DICOM image file
        const double imgWidth   = 200.0;            // desired width in points (1/72 inch)
        const double imgHeight  = 150.0;            // desired height in points

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

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (or any page you want to place the image on)
            Page page = doc.Pages[1];

            // Create an Image object for the DICOM file and set custom dimensions (points)
            Image img = new Image
            {
                File = dicomImage,
                FixWidth = imgWidth,
                FixHeight = imgHeight
            };

            // Add the image to the page's paragraph collection
            page.Paragraphs.Add(img);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with DICOM image saved to '{outputPdf}'.");
    }
}
