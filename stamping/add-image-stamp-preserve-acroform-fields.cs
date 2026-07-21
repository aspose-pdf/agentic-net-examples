using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath  = "output.pdf";

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

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in pdfDoc.Pages)
            {
                // Create an image stamp from the specified file
                ImageStamp imgStamp = new ImageStamp(stampImagePath);

                // Configure stamp appearance (optional)
                imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
                imgStamp.VerticalAlignment   = VerticalAlignment.Bottom;
                // Use XIndent/YIndent instead of the non‑existent Margin property
                imgStamp.XIndent = 10; // distance from the right edge when Right alignment is used
                imgStamp.YIndent = 10; // distance from the bottom edge when Bottom alignment is used
                imgStamp.Opacity = 0.5f; // semi‑transparent

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; AcroForm fields are preserved automatically
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}
