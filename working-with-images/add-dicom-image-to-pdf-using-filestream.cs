using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source DICOM image and the output PDF.
        const string dicomImagePath = "medical_image.dcm"; // replace with actual image file
        const string outputPdfPath  = "output.pdf";

        // Ensure the image file exists.
        if (!File.Exists(dicomImagePath))
        {
            Console.Error.WriteLine($"Image file not found: {dicomImagePath}");
            return;
        }

        // Create a new PDF document and add a single page.
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add();

            // Load the image via a FileStream and assign it to an Image object.
            using (FileStream imgStream = File.OpenRead(dicomImagePath))
            {
                Image img = new Image();          // parameterless constructor
                img.ImageStream = imgStream;      // set the stream containing the image data
                // Optionally, you can set the image dimensions or scaling here.
                // img.FixWidth  = 300;
                // img.FixHeight = 400;

                // Add the image to the first page.
                pdfDoc.Pages[1].Paragraphs.Add(img);
            }

            // Save the PDF document.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with DICOM image saved to '{outputPdfPath}'.");
    }
}