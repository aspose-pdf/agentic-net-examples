using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string dicomPath = "medical.dcm";   // Path to the DICOM image file
        const string outputPdf = "output.pdf";    // Desired PDF output path

        if (!File.Exists(dicomPath))
        {
            Console.Error.WriteLine($"File not found: {dicomPath}");
            return;
        }

        // Keep the DICOM stream open until the PDF is saved
        using (FileStream dicomStream = new FileStream(dicomPath, FileMode.Open, FileAccess.Read))
        {
            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a blank page to the document
                pdfDoc.Pages.Add();

                // Create an Image object and assign the DICOM stream
                Image dicomImage = new Image();
                dicomImage.ImageStream = dicomStream;   // Use the stream as the image source

                // Optionally set size or scaling (commented out)
                // dicomImage.FixWidth = 500;
                // dicomImage.FixHeight = 500;

                // Add the image to the first page
                pdfDoc.Pages[1].Paragraphs.Add(dicomImage);

                // Save the PDF document
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with DICOM image saved to '{outputPdf}'.");
    }
}