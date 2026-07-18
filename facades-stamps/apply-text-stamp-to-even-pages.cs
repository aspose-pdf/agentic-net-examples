using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_even_pages.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the document to determine the total number of pages.
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count; // 1‑based indexing

            // Build an array containing only the even page numbers.
            int evenCount = pageCount / 2;
            int[] evenPages = new int[evenCount];
            int idx = 0;
            for (int i = 2; i <= pageCount; i += 2)
            {
                evenPages[idx++] = i;
            }

            // Create a text stamp that will be applied to the even pages.
            FormattedText ft = new FormattedText(
                "Even Page Aspose.Pdf.Facades.Stamp",                     // text
                System.Drawing.Color.Gray,             // text color (System.Drawing.Color is required here)
                "Helvetica",                           // font name
                EncodingType.Winansi,                  // encoding
                false,                                 // embed font
                36);                                   // font size

            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(ft);          // bind the formatted text to the stamp
            stamp.IsBackground = true;   // place the stamp behind page content
            stamp.Pages = evenPages;     // restrict stamping to even pages only

            // Apply the stamp using PdfFileStamp facade.
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPdf);          // load source PDF
            fileStamp.AddStamp(stamp);            // add the configured stamp
            fileStamp.Save(outputPdf);            // write result
            fileStamp.Close();                    // release resources
        }

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp applied to even pages. Output saved to '{outputPdf}'.");
    }
}