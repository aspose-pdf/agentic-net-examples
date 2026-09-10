using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "portrait.pdf";
        const string outputPdf = "landscape.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF with PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPdf);

            // Retrieve original size of the first page (pages are 1‑based)
            int pageNumber = 1;
            PageSize originalSize = editor.GetPageSize(pageNumber);
            Console.WriteLine($"Original size (points): {originalSize.Width} x {originalSize.Height}");

            // Set the output page size to landscape by swapping width and height
            editor.PageSize = new PageSize(originalSize.Height, originalSize.Width);

            // Rotate the page content 90° so it fits the new orientation
            editor.Rotation = 90;

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPdf);
        }

        // Verify dimensions and rotation of the resulting PDF
        using (Document resultDoc = new Document(outputPdf))
        {
            Page resultPage = resultDoc.Pages[1]; // first page
            Console.WriteLine($"Result size (points): {resultPage.PageInfo.Width} x {resultPage.PageInfo.Height}");
            Console.WriteLine($"Result rotation (degrees): {resultPage.Rotate}");
        }
    }
}