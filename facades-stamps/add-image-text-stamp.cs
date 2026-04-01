using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logoImage = "logo.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoImage))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImage}");
            return;
        }

        // Create a stamp that contains both an image and custom text
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(logoImage);

        FormattedText formattedText = new FormattedText(
            "My Company",
            System.Drawing.Color.Blue,
            "Helvetica",
            EncodingType.Winansi,
            false,
            24);
        stamp.BindLogo(formattedText);

        stamp.SetOrigin(100, 400);
        stamp.IsBackground = true;
        stamp.Opacity = 0.7f;

        // Apply the stamp to all pages of the PDF
        using (PdfFileStamp pdfFileStamp = new PdfFileStamp())
        {
            pdfFileStamp.BindPdf(inputPdf);
            pdfFileStamp.AddStamp(stamp);
            pdfFileStamp.Save(outputPdf);
            pdfFileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
