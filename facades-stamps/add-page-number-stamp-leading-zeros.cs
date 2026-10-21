using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using Document (PdfFileEditor does not support BindPdf)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a page‑number stamp with leading zeros (e.g., 01, 02, …)
            PageNumberStamp pageNumberStamp = new PageNumberStamp("00")
            {
                StartingNumber = 1,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                // Use individual margin properties – PageNumberStamp has no combined Margin property
                BottomMargin = 10,
                LeftMargin = 0,
                RightMargin = 0,
                TopMargin = 0
            };

            // Apply the stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the stamped PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Page‑number stamped PDF saved to '{outputPath}'.");
    }
}
