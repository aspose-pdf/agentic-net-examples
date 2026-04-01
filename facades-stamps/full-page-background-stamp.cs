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

        // Load the PDF to obtain page dimensions (assumes all pages have the same size)
        float pageWidth;
        float pageHeight;
        using (Document doc = new Document(inputPath))
        {
            Page firstPage = doc.Pages[1];
            pageWidth = (float)firstPage.PageInfo.Width;
            pageHeight = (float)firstPage.PageInfo.Height;
        }

        // Configure the stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(stampImagePath);
        stamp.IsBackground = true;
        stamp.Opacity = 0.5f;
        stamp.SetOrigin(0f, 0f);
        stamp.SetImageSize(pageWidth, pageHeight);

        // Apply the stamp to all pages of the document
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Background stamp applied and saved to '{outputPath}'.");
    }
}
