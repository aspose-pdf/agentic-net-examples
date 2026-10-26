using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";   // existing PDF to insert the image into
        const string dicomImagePath = "image.dcm";   // DICOM image file
        const string outputPdfPath = "output.pdf";   // result PDF

        // Verify input files exist
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

        // Document lifecycle must be wrapped in a using block (deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Load the DICOM image via ImageStream (Aspose.Pdf.Image has no ctor that accepts a path)
            using (FileStream imgStream = new FileStream(dicomImagePath, FileMode.Open, FileAccess.Read))
            {
                Image dicomImage = new Image
                {
                    ImageStream = imgStream,
                    // Adjust resolution by setting explicit width and height (points, 1/72 inch)
                    FixWidth = 300,   // desired width
                    FixHeight = 200   // desired height
                };

                // Add the image to the first page (Aspose.Pdf uses 1‑based page indexing)
                Page firstPage = doc.Pages[1];
                firstPage.Paragraphs.Add(dicomImage);

                // Save the modified PDF (no SaveOptions needed for PDF output)
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"DICOM image inserted and saved to '{outputPdfPath}'.");
    }
}
