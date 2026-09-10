using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Ensure an input PDF exists – create a minimal one‑page PDF
        //    because the sandbox does not contain external files.
        // ------------------------------------------------------------
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // ------------------------------------------------------------
        // 2. Load the PDF from a memory stream (as the original task
        //    requires).
        // ------------------------------------------------------------
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (var inputStream = new MemoryStream(pdfBytes))
        using (var doc = new Document(inputStream))
        {
            // --------------------------------------------------------
            // 3. Prepare the multi‑line watermark text with custom font
            //    size and colour.
            // --------------------------------------------------------
            string watermarkText = "Confidential\nDo Not Distribute";
            var formattedText = new Aspose.Pdf.Facades.FormattedText(
                watermarkText,
                System.Drawing.Color.Red,               // text colour
                "Helvetica",                           // font name
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,                                   // embed font flag
                48f);                                    // font size

            // --------------------------------------------------------
            // 4. Create a stamp, bind the formatted text and set its
            //    appearance (background, opacity, position).
            // --------------------------------------------------------
            var stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(formattedText);
            stamp.IsBackground = true;   // place behind page content
            stamp.Opacity = 0.3f;        // semi‑transparent
            stamp.SetOrigin(100f, 400f); // X, Y position on the page

            // --------------------------------------------------------
            // 5. Apply the stamp to all pages of the document.
            // --------------------------------------------------------
            var fileStamp = new Aspose.Pdf.Facades.PdfFileStamp(doc);
            fileStamp.AddStamp(stamp);

            // --------------------------------------------------------
            // 6. Save the watermarked PDF.
            // --------------------------------------------------------
            fileStamp.Save("watermarked.pdf");
            fileStamp.Close();
        }
    }
}
