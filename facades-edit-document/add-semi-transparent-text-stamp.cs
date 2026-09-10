using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source PDF exists – create a minimal one‑page PDF if it does not.
        if (!File.Exists(inputPdf))
        {
            using (var placeholder = new Aspose.Pdf.Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPdf);
            }
        }

        // Initialize the facade that works with PDF files
        using (PdfFileStamp pdfStamp = new PdfFileStamp())
        {
            // Load the source PDF
            pdfStamp.BindPdf(inputPdf);

            // Create a formatted text object (text, color, font, encoding, embedded, size)
            Aspose.Pdf.Facades.FormattedText formatted = new Aspose.Pdf.Facades.FormattedText(
                "Confidential",                // text to display
                System.Drawing.Color.Red,       // text color (fully‑qualified System.Drawing.Color)
                "Helvetica",                  // font name
                Aspose.Pdf.Facades.EncodingType.Winansi, // encoding (fully‑qualified)
                false,                         // not embedded
                36);                           // font size

            // Create a stamp, bind the formatted text, and set opacity to 0.5 (50% transparent)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(formatted);
            stamp.Opacity = 0.5f;            // partial transparency (0 = fully transparent, 1 = opaque)
            stamp.IsBackground = true;      // optional: place stamp behind page content

            // Add the stamp to the PDF
            pdfStamp.AddStamp(stamp);

            // Save the modified PDF
            pdfStamp.Save(outputPdf);
        }

        Console.WriteLine($"Annotation with 0.5 opacity saved to '{outputPdf}'.");
    }
}
