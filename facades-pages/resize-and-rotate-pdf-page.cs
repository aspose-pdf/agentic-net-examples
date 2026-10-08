using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Page numbers are 1‑based in Aspose.Pdf
        int pageNumber = 1;
        Page page = pdfDoc.Pages[pageNumber];

        // Retrieve the current page dimensions
        double originalWidth = page.PageInfo.Width;
        double originalHeight = page.PageInfo.Height;

        // Define new dimensions (example: shrink to 80% of original size)
        double scaleFactor = 0.8;
        double newWidth = originalWidth * scaleFactor;
        double newHeight = originalHeight * scaleFactor;

        // Resize the page itself
        page.SetPageSize(newWidth, newHeight);

        // Compute margins that will effectively shrink the existing content to the same scale.
        // The margins are the difference between the original and new size divided by two.
        double marginLeft = (originalWidth - newWidth) / 2;
        double marginTop = (originalHeight - newHeight) / 2;
        double marginRight = marginLeft;
        double marginBottom = marginTop;

        // Resize the page contents using the margins‑based overload.
        int[] pages = new int[] { pageNumber }; // 1‑based page numbers
        var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(
            marginLeft, marginTop, marginRight, marginBottom);

        new PdfFileEditor().ResizeContents(pdfDoc, pages, resizeParams);

        // Rotate the page 90 degrees clockwise
        page.Rotate = Rotation.on90;

        // Persist the final PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Page {pageNumber} resized and rotated saved to '{outputPath}'.");
    }
}
