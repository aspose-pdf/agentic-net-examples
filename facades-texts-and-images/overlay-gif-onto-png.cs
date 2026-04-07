using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pngPath = "image.png";
        const string gifPath = "overlay.gif";
        const string basePdf = "base.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(pngPath))
        {
            Console.Error.WriteLine("PNG file not found: " + pngPath);
            return;
        }
        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine("GIF file not found: " + gifPath);
            return;
        }

        // Create a PDF containing the PNG image
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            Aspose.Pdf.Image pngImage = new Aspose.Pdf.Image();
            pngImage.File = pngPath;
            pngImage.FixWidth = page.PageInfo.Width;
            pngImage.FixHeight = page.PageInfo.Height;
            page.Paragraphs.Add(pngImage);
            doc.Save(basePdf);
        }

        // Overlay the semi‑transparent GIF using PdfFileMend and CompositingParameters
        PdfFileMend mender = new PdfFileMend(basePdf, outputPdf);
        using (FileStream gifStream = File.OpenRead(gifPath))
        {
            CompositingParameters compParams = new CompositingParameters(BlendMode.Normal);
            float lowerLeftX = 100f;
            float lowerLeftY = 100f;
            float upperRightX = 300f;
            float upperRightY = 300f;
            mender.AddImage(gifStream, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY, compParams);
        }
        mender.Close();

        Console.WriteLine("Overlay completed. Output saved to " + outputPdf);
    }
}
