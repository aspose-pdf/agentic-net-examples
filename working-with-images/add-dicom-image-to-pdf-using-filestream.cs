using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string dicomPath = "image.dcm";   // Path to the DICOM file
        const string outputPdf = "output.pdf"; // Destination PDF file

        // Verify that the DICOM file exists
        if (!File.Exists(dicomPath))
        {
            Console.Error.WriteLine($"File not found: {dicomPath}");
            return;
        }

        // Open the DICOM file as a read‑only stream
        using (FileStream dicomStream = File.OpenRead(dicomPath))
        {
            // Create a new PDF document (empty)
            using (Document pdfDoc = new Document())
            {
                // Add a blank page to the document
                pdfDoc.Pages.Add();

                // Create an Image object and assign the DICOM stream
                Image img = new Image();
                img.ImageStream = dicomStream; // Image constructor does not accept a path; use ImageStream

                // Optional: set image dimensions and alignment on the page
                img.FixWidth = 500;   // width in points
                img.FixHeight = 500;  // height in points
                img.HorizontalAlignment = HorizontalAlignment.Center;
                img.VerticalAlignment = VerticalAlignment.Center;

                // Add the image to the first page's paragraph collection
                pdfDoc.Pages[1].Paragraphs.Add(img);

                // Save the resulting PDF
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with DICOM image saved to '{outputPdf}'.");
    }
}