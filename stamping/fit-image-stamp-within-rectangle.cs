using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp lives here
using Aspose.Pdf.Annotations; // HorizontalAlignment, VerticalAlignment enums

// Alias to disambiguate the two Rectangle types
using PdfRect = Aspose.Pdf.Rectangle;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg = "stamp.png";

        if (!File.Exists(inputPdf)) { Console.Error.WriteLine($"Input PDF not found: {inputPdf}"); return; }
        if (!File.Exists(stampImg)) { Console.Error.WriteLine($"Stamp image not found: {stampImg}"); return; }

        // Define the rectangle where the stamp should be placed (coordinates in points)
        PdfRect targetRect = new PdfRect(100, 100, 300, 250);

        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp stamp = new ImageStamp(stampImg)
            {
                // Size the stamp to exactly match the rectangle dimensions
                Width = targetRect.URX - targetRect.LLX,
                Height = targetRect.URY - targetRect.LLY,

                // Position the stamp inside the rectangle
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                XIndent = targetRect.LLX,
                YIndent = targetRect.LLY,

                // Ensure the stamp is drawn over the page content (foreground)
                Background = false
            };

            // Apply the stamp to the first page (adjust index if needed)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}