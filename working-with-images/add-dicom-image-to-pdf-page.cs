using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the DICOM image and the output PDF
        const string dicomPath = "medical_image.dcm";
        const string outputPdf = "output.pdf";

        // Verify that the DICOM file exists
        if (!File.Exists(dicomPath))
        {
            Console.Error.WriteLine($"DICOM file not found: {dicomPath}");
            return;
        }

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a blank page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Load the DICOM image from a FileStream
            using (FileStream fs = new FileStream(dicomPath, FileMode.Open, FileAccess.Read))
            {
                // Create an Image object and assign the stream
                Aspose.Pdf.Image dicomImage = new Aspose.Pdf.Image();
                dicomImage.ImageStream = fs;

                // Fit the image to the page width (optional: also fit height)
                dicomImage.FixWidth = page.PageInfo.Width;
                // dicomImage.FixHeight = page.PageInfo.Height; // uncomment if you want to fit height as well

                // Add the image to the page's paragraph collection
                page.Paragraphs.Add(dicomImage);
            }

            // Save the PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"DICOM image added and PDF saved to '{outputPdf}'.");
    }
}
