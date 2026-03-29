using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string tempPath = "temp.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Add the stamp to the document (initial position at (0,0)).
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);

            // Use the Facades.Stamp class and bind the image to it.
            Aspose.Pdf.Facades.Stamp imgStamp = new Aspose.Pdf.Facades.Stamp();
            imgStamp.BindImage(stampImagePath);
            imgStamp.IsBackground = false;
            imgStamp.SetOrigin(0, 0); // initial position; will be moved later.

            fileStamp.AddStamp(imgStamp);
            fileStamp.Save(tempPath);
        }

        // Move the stamp to the center of page 8.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(tempPath);

            using (Document doc = new Document(tempPath))
            {
                if (doc.Pages.Count < 8)
                {
                    Console.Error.WriteLine("Document has less than 8 pages.");
                    return;
                }

                Page page = doc.Pages[8];
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Move the first stamp (index 1) on page 8 to the calculated center.
                // Note: MoveStamp positions the lower‑left corner of the stamp, so the stamp will be offset
                // by half of its width/height if true centering is required. For this example we place the
                // origin at the page centre.
                editor.MoveStamp(8, 1, centerX, centerY);
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Stamp centered on page 8 saved to '{outputPath}'.");
    }
}
