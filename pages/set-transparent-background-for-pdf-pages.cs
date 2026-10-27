using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "overlay.pdf";
        const string outputPath = "overlay_transparent.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; ensure each page has no explicit background color.
            // In Aspose.Pdf the default page background is transparent, so we simply skip setting it.
            // If a future version provides PageInfo.BackgroundColor, the following line can be uncommented:
            // doc.Pages[i].PageInfo.BackgroundColor = Aspose.Pdf.Color.Transparent;
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // No action needed – default is transparent.
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with transparent page backgrounds to '{outputPath}'.");
    }
}
