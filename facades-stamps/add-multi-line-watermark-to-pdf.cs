using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF in memory – this replaces the missing "input.pdf"
        // ---------------------------------------------------------------------
        using (var seedDoc = new Document())
        {
            seedDoc.Pages.Add(); // add a blank page (you can add content if you wish)

            using (var seedStream = new MemoryStream())
            {
                seedDoc.Save(seedStream);
                seedStream.Position = 0; // rewind so PdfFileStamp can read from the start

                // ---------------------------------------------------------------
                // 2. Apply a multi‑line watermark using PdfFileStamp (stream overload)
                // ---------------------------------------------------------------
                using (var fileStamp = new Aspose.Pdf.Facades.PdfFileStamp())
                {
                    fileStamp.BindPdf(seedStream);

                    string watermarkText = "Confidential\nDo Not Distribute";

                    // FormattedText: text, color, font name, encoding, embed flag, font size
                    var formatted = new Aspose.Pdf.Facades.FormattedText(
                        watermarkText,
                        System.Drawing.Color.Red,
                        "Helvetica",
                        Aspose.Pdf.Facades.EncodingType.Winansi,
                        false,
                        48);

                    var stamp = new Aspose.Pdf.Facades.Stamp();
                    stamp.BindLogo(formatted);
                    stamp.SetOrigin(100, 400);   // lower‑left corner of the page
                    stamp.Opacity = 0.5f;        // semi‑transparent
                    stamp.IsBackground = true;   // render behind existing content

                    fileStamp.AddStamp(stamp);

                    // -----------------------------------------------------------
                    // 3. Save the result – here we write to a file for demonstration
                    // -----------------------------------------------------------
                    using (var outputStream = new MemoryStream())
                    {
                        fileStamp.Save(outputStream);
                        File.WriteAllBytes("watermarked.pdf", outputStream.ToArray());
                    }
                }
            }
        }
    }
}
