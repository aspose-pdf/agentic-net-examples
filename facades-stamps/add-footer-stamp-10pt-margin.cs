using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
// Removed Aspose.Pdf.Text to avoid type conflict with FormattedText in Facades

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Create a simple PDF if it does not exist
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        // Add a footer stamp positioned 10 points above the bottom edge
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);

            // Use System.Drawing.Color and a float font size; fully qualify FormattedText to avoid ambiguity
            Aspose.Pdf.Facades.FormattedText footerText = new Aspose.Pdf.Facades.FormattedText(
                "Footer text",
                System.Drawing.Color.Black,
                "Helvetica",
                EncodingType.Winansi,
                false,
                12f);

            // Bottom margin of 10 points
            fileStamp.AddFooter(footerText, 10f);

            fileStamp.Save(outputPath);
        }

        Console.WriteLine("Footer stamp added. Output saved to '" + outputPath + "'.");
    }
}
