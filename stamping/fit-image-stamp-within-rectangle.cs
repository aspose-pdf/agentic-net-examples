using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for StampAnnotation if needed (not used here)

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and image to be stamped
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "logo.png";

        // Define the rectangle where the stamp should be placed (coordinates in points)
        // Rectangle(left, bottom, right, top)
        Aspose.Pdf.Rectangle stampRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

        // Ensure the source files exist
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

        // Load the PDF document, modify, and save using a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Get the first page (1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Set stamp dimensions to match the defined rectangle
            imgStamp.Width  = stampRect.URX - stampRect.LLX; // rectangle width
            imgStamp.Height = stampRect.URY - stampRect.LLY; // rectangle height

            // Position the stamp at the rectangle's lower‑left corner
            imgStamp.XIndent = stampRect.LLX;
            imgStamp.YIndent = stampRect.LLY;

            // Optional: make the stamp appear behind page content
            imgStamp.Background = false; // true = background, false = foreground

            // Add the stamp to the page
            page.AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}