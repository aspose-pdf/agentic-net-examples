using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // -------------------------------------------------
        // 1. Create a simple source PDF in memory (seed PDF)
        // -------------------------------------------------
        using (var seedDoc = new Aspose.Pdf.Document())
        {
            // add a blank page
            seedDoc.Pages.Add();

            // optional: add some sample text so we can see the watermark effect
            var paragraph = new Aspose.Pdf.Text.TextFragment("Sample PDF content");
            seedDoc.Pages[1].Paragraphs.Add(paragraph);

            // save the seed PDF into a memory stream
            using (var sourceStream = new MemoryStream())
            {
                seedDoc.Save(sourceStream);
                sourceStream.Position = 0; // reset for reading

                // -------------------------------------------------
                // 2. Load the PDF from the memory stream
                // -------------------------------------------------
                using (var doc = new Aspose.Pdf.Document(sourceStream))
                {
                    // Multi‑line watermark text
                    string watermarkText = "Confidential\nDo Not Distribute";

                    // Create formatted text with custom color, font, encoding, embed flag and size
                    var formattedText = new Aspose.Pdf.Facades.FormattedText(
                        watermarkText,
                        System.Drawing.Color.Red,          // custom color (System.Drawing)
                        "Helvetica",                      // custom font name
                        Aspose.Pdf.Facades.EncodingType.Winansi,
                        false,
                        48);                               // custom font size

                    // Prepare an array with all page numbers (apply watermark to every page)
                    int pageCount = doc.Pages.Count;
                    int[] allPages = new int[pageCount];
                    for (int i = 0; i < pageCount; i++)
                        allPages[i] = i + 1; // pages are 1‑based

                    // Use PdfFileMend (facade) to add the text watermark
                    var mend = new Aspose.Pdf.Facades.PdfFileMend();
                    mend.BindPdf(doc);

                    // Define the rectangle where the watermark will be placed (llx, lly, urx, ury)
                    // Adjust these values as needed for your layout
                    float llx = 200f;
                    float lly = 400f;
                    float urx = 500f;
                    float ury = 600f;

                    mend.AddText(formattedText, allPages, llx, lly, urx, ury);
                    mend.Save("watermarked.pdf");
                    mend.Close();
                }
            }
        }
    }
}
