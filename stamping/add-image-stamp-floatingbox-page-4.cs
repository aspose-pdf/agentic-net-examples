using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg = "stamp.png";

        if (!File.Exists(inputPdf) || !File.Exists(stampImg))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Verify that page 4 exists (pages are 1‑based)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            Page page = doc.Pages[4];

            // Create a FloatingBox sized to the page (constructor expects float values)
            FloatingBox box = new FloatingBox((float)page.PageInfo.Width, (float)page.PageInfo.Height)
            {
                Left = 0,
                Top = 0
            };

            // Load the image and assign it as the background of the FloatingBox
            Image background = new Image { File = stampImg };
            box.BackgroundImage = background;

            // Add the FloatingBox to the page's paragraph collection
            page.Paragraphs.Add(box);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added as background in a FloatingBox on page 4. Saved to '{outputPdf}'.");
    }
}
