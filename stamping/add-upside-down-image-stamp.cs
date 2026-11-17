using System;
using System.IO;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "stamped_output.pdf";
        const string stampImagePath = "stamp.png";
        const int    targetPageNumber = 1; // 1‑based page index

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // Create an image stamp from the PNG/JPG file
                Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(stampImagePath);

                // Rotate the stamp 180 degrees so it appears upside‑down
                imgStamp.RotateAngle = 180;

                // Optional: position the stamp (centered on the page)
                imgStamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                imgStamp.VerticalAlignment   = Aspose.Pdf.VerticalAlignment.Center;

                // Retrieve the target page (pages are 1‑based)
                Aspose.Pdf.Page targetPage = pdfDoc.Pages[targetPageNumber];

                // Add the stamp to the page
                targetPage.AddStamp(imgStamp);

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}