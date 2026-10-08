using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_margin.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF to read original page dimensions.
        using (Document doc = new Document(inputPath))
        {
            // Assume uniform page size; get dimensions of the first page.
            var firstPage = doc.Pages[1];
            double origWidth = firstPage.PageInfo.Width;
            double origHeight = firstPage.PageInfo.Height;

            // 20% margin on each side.
            double marginX = origWidth * 0.20;
            double marginY = origHeight * 0.20;

            // New page size includes margins on both sides.
            double newWidth = origWidth + 2 * marginX;
            double newHeight = origHeight + 2 * marginY;

            // Resize every page and add the calculated margins.
            foreach (Page page in doc.Pages)
            {
                // Set the new page dimensions.
                page.PageInfo.Width = newWidth;
                page.PageInfo.Height = newHeight;

                // Define the whitespace (margin) around the original content.
                page.PageInfo.Margin = new MarginInfo
                {
                    Left = marginX,
                    Right = marginX,
                    Top = marginY,
                    Bottom = marginY
                };
            }

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with 20% margins: {outputPath}");
    }
}
