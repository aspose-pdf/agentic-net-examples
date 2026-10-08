using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string stampImagePath = "stamp.png";

        // Define the rectangle where the stamp should fit (coordinates in points)
        // Lower‑left (llx, lly) and upper‑right (urx, ury) corners.
        const double llx = 100;   // left X
        const double lly = 500;   // lower Y
        const double urx = 300;   // right X
        const double ury = 700;   // upper Y

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

        // Use a using block for deterministic disposal of the Document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a fully qualified Rectangle to avoid ambiguity.
            Aspose.Pdf.Rectangle targetRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create the image stamp.
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Position the stamp at the rectangle's lower‑left corner.
                LeftMargin   = targetRect.LLX,
                BottomMargin = targetRect.LLY,

                // Explicitly set the stamp size to match the rectangle.
                Width  = targetRect.Width,
                Height = targetRect.Height,

                // Optional: ensure the stamp is placed on top of page content.
                Background = false
            };

            // Apply the stamp to the first page (or any page you need).
            Page page = pdfDoc.Pages[1];
            page.AddStamp(imgStamp);

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPdfPath}'.");
    }
}
