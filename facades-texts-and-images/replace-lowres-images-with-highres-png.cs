using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string highResImage = "highres.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(highResImage))
        {
            Console.Error.WriteLine($"High‑resolution image not found: {highResImage}");
            return;
        }

        // Load the PDF document (using rule for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Bind the document to the PdfContentEditor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Get the number of images on the current page
                int imageCount = doc.Pages[pageNum].Resources.Images.Count;

                // Replace each image with the high‑resolution PNG
                for (int imgIdx = 1; imgIdx <= imageCount; imgIdx++)
                {
                    editor.ReplaceImage(pageNum, imgIdx, highResImage);
                }
            }

            // Save the modified PDF (using rule for saving)
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with high‑resolution images to '{outputPdf}'.");
    }
}