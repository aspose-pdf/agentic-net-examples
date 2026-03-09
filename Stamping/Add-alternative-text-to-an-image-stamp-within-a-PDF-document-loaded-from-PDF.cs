using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and image files
        const string inputPdfPath  = "input.pdf";
        const string imagePath     = "stamp.png";
        const string outputPdfPath = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Set alternative text for accessibility
            imgStamp.AlternativeText = "Company logo – used as a header graphic";

            // Position the stamp (example: top‑right corner) using alignment and indent values
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;
            // Use XIndent/YIndent instead of the non‑existent Margin property
            imgStamp.XIndent = 10; // distance from the right edge when Right alignment is used
            imgStamp.YIndent = 10; // distance from the top edge when Top alignment is used

            // Add the stamp to the first page
            pdfDoc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with image stamp and alt text: {outputPdfPath}");
    }
}
