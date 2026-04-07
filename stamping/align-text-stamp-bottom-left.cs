using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text stamp with the desired content
                TextStamp stamp = new TextStamp("Sample Text");

                // Align to bottom‑left corner with a 10‑point margin
                stamp.HorizontalAlignment = HorizontalAlignment.Left;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                stamp.LeftMargin          = 10; // points from the left edge
                stamp.BottomMargin        = 10; // points from the bottom edge

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}