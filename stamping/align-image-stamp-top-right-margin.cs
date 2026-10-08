using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output.pdf";

        // Verify files exist
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
        using (Document pdf = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= pdf.Pages.Count; i++)
            {
                Page page = pdf.Pages[i];

                // Create an ImageStamp and configure alignment to top‑right
                ImageStamp stamp = new ImageStamp(stampImg)
                {
                    // Align to the right edge and top edge of the page
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Top,

                    // Margin offsets from the top and right edges (in points)
                    TopMargin  = 30, // distance from the top edge
                    RightMargin = 20  // distance from the right edge
                };

                // Apply the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF; Save() is called while the Document is still alive
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}