using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // for ImageStamp (inherits from Stamp)
using Aspose.Pdf.Text;          // for Position if needed (not required for ImageStamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output_accessible.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the PNG file
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position the stamp (optional – here centered on the page)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Set the alternative text with Unicode characters for multilingual accessibility
                AlternativeText = "图像说明 – صورة توضيحية – Image description"
            };

            // Apply the stamp to every page (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPdf}'.");
    }
}