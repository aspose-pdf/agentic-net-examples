using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // HorizontalAlignment, VerticalAlignment enums
using Aspose.Pdf.Drawing;    // ImageStamp class

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string patternImg = "pattern.png"; // background pattern image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(patternImg))
        {
            Console.Error.WriteLine($"Pattern image not found: {patternImg}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Iterate pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create an ImageStamp for the background pattern
                    ImageStamp stamp = new ImageStamp(patternImg)
                    {
                        // Place behind existing page content
                        Background = true,
                        // 5 percent opacity for a subtle effect
                        Opacity   = 0.05f,
                        // Size to fill the whole page
                        Width  = page.PageInfo.Width,
                        Height = page.PageInfo.Height,
                        // Position at the origin (lower‑left corner)
                        XIndent = 0,
                        YIndent = 0,
                        // Ensure the stamp does not affect existing content layout
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment   = VerticalAlignment.Bottom
                    };

                    // Add the stamp to the current page
                    page.AddStamp(stamp);
                }

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Background pattern applied and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
