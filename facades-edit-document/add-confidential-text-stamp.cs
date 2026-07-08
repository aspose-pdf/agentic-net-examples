using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file into the facade
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create a text stamp with the desired appearance
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Make the stamp a background element and set semi‑transparent opacity
        stamp.IsBackground = true;
        stamp.Opacity = 0.5f; // 50 % opacity

        // Define the formatted text: "Confidential" in red Helvetica, size 36
        FormattedText formattedText = new FormattedText(
            "Confidential",
            System.Drawing.Color.Red,   // FormattedText requires System.Drawing.Color
            "Helvetica",
            EncodingType.Winansi,
            false,
            36);

        // Bind the formatted text to the stamp
        stamp.BindLogo(formattedText);

        // Add the stamp to all pages (null Pages property means all pages)
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}