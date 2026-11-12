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
        const string stampImg = "footer.png";

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

        // Load the PDF document using the Document class (PdfFileEditor has no BindPdf)
        Document pdfDoc = new Document(inputPdf);

        // Create an ImageStamp from Aspose.Pdf.Drawing namespace
        ImageStamp stamp = new ImageStamp(stampImg)
        {
            // Center horizontally
            HorizontalAlignment = HorizontalAlignment.Center,
            // Align to the bottom of the page; YIndent is the distance from the bottom edge
            VerticalAlignment = VerticalAlignment.Bottom,
            YIndent = 10,               // 10 points above the bottom edge
            Background = false         // place stamp in the foreground (over content)
        };

        // Apply the stamp to every page by iterating over Document.Pages
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the stamped PDF
        pdfDoc.Save(outputPdf);
    }
}
