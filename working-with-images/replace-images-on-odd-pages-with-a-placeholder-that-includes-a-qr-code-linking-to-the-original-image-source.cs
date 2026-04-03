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
        const string placeholderImage = "qr_placeholder.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(placeholderImage))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImage}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Bind the document to the content editor
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(pdfDoc);

            // Iterate through pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Process only odd pages
                if (pageNumber % 2 == 0) continue;

                // Get the number of images on the current page
                int imageCount = pdfDoc.Pages[pageNumber].Resources.Images.Count;

                // Replace each image with the QR‑code placeholder
                for (int imgIndex = 1; imgIndex <= imageCount; imgIndex++)
                {
                    editor.ReplaceImage(pageNumber, imgIndex, placeholderImage);
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}