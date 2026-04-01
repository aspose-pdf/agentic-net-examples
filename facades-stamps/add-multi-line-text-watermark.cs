using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Create formatted text with the first line of the watermark
        // NOTE: Use System.Drawing.Color for the color argument and a float for the font size.
        var formattedText = new Aspose.Pdf.Facades.FormattedText(
            "Confidential",
            System.Drawing.Color.Gray,
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            48f);

        // Add additional lines using AddNewLineText
        formattedText.AddNewLineText("Do not distribute");
        formattedText.AddNewLineText("Company Internal Use Only");

        // Create a stamp and bind the formatted text to it
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formattedText);
        stamp.IsBackground = true;
        stamp.Opacity = 0.5f;
        stamp.SetOrigin(100f, 400f);

        // Apply the stamp to the PDF file
        PdfFileStamp pdfFileStamp = new PdfFileStamp();
        pdfFileStamp.BindPdf(inputPath);
        pdfFileStamp.AddStamp(stamp);
        pdfFileStamp.Save(outputPath);
        pdfFileStamp.Close();

        Console.WriteLine("Watermarked PDF saved to '" + outputPath + "'.");
    }
}
