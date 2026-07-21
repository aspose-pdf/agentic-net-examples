using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "stamped_output.pdf";
        const string logoImage  = "logo.png";

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

        // Initialize the facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);

            // ---------- Image stamp (company logo) ----------
            Stamp imageStamp = new Stamp();
            // Position the logo (example coordinates)
            imageStamp.SetOrigin(50f, 750f);          // X, Y from bottom-left
            // Desired size of the logo
            imageStamp.SetImageSize(100f, 50f);       // Width, Height
            // Bind the image file
            imageStamp.BindImage(logoImage);
            // Add the image stamp to the document
            fileStamp.AddStamp(imageStamp);

            // ---------- Text stamp (Confidential) ----------
            // FormattedText constructor requires System.Drawing.Color
            // Bold text is achieved by using a bold font (e.g., Helvetica-Bold)
            FormattedText ft = new FormattedText(
                "Confidential",                     // text
                System.Drawing.Color.Red,           // text color
                "Helvetica-Bold",                   // font name (bold)
                EncodingType.Winansi,               // encoding
                false,                              // isEmbedded
                36);                                // font size

            Stamp textStamp = new Stamp();
            // Position the text (example coordinates)
            textStamp.SetOrigin(200f, 750f);
            // Bind the formatted text as a stamp
            textStamp.BindLogo(ft);
            // Add the text stamp to the document
            fileStamp.AddStamp(textStamp);

            // Save the stamped PDF
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}