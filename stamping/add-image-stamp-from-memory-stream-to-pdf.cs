using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "stamp.png";

        // Verify required files exist
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

        // Load the image into a memory stream (no file path used for the stamp)
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            // Create an ImageStamp from the memory stream
            ImageStamp stamp = new ImageStamp(imageStream)
            {
                Background          = false,                     // place stamp over content
                Opacity             = 0.5,                       // semi‑transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Open the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Apply the stamp to each page individually (Page.AddStamp, not PageCollection)
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
    }
}