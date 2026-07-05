using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath = "logo.png";
        const string stampText = "Confidential";

        if (!File.Exists(inputPdf) || !File.Exists(logoPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Create a stamp instance (Facades API)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Position and size of the stamp on the page
            stamp.SetOrigin(100, 500);          // X and Y coordinates (from bottom‑left)
            stamp.SetImageSize(100, 100);       // Width and height of the image part

            // Bind the logo image
            stamp.BindImage(logoPath);

            // Create formatted text (uses System.Drawing.Color as required by the API)
            FormattedText formattedText = new FormattedText(
                stampText,                         // text to display
                System.Drawing.Color.Red,          // text color
                "Helvetica",                       // font name
                EncodingType.Winansi,              // encoding
                false,                             // embedded flag
                24);                               // font size

            // Bind the text to the same stamp
            stamp.BindLogo(formattedText);

            // Optional visual settings
            stamp.IsBackground = false;
            stamp.Opacity = 0.7f;

            // Apply the stamp to the document using PdfFileStamp
            using (Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp())
            {
                fileStamp.BindPdf(doc);
                fileStamp.AddStamp(stamp);
                fileStamp.Save(outputPdf);
                fileStamp.Close();
            }
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}