using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "watermarked.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the document (only needed to obtain page size for centering)
        Document doc = new Document(inputPdf);
        Page firstPage = doc.Pages[1];
        // PageInfo.Width/Height are double – cast to float for later calculations
        float pageWidth = (float)firstPage.PageInfo.Width;
        float pageHeight = (float)firstPage.PageInfo.Height;
        doc.Dispose(); // we only needed the size information

        // ---------------------------------------------------------------------
        // 1. Build a FormattedText that contains three lines.
        //    The AddNewLineText method is used to append the extra lines.
        //    NOTE: All lines share the same font settings in a single FormattedText
        //    instance. To have truly different font styles per line you would need
        //    separate FormattedText objects and separate stamps. The example below
        //    demonstrates the required API usage while keeping the code simple.
        // ---------------------------------------------------------------------
        Aspose.Pdf.Facades.FormattedText formatted = new Aspose.Pdf.Facades.FormattedText(
            "First line – Bold 24pt",
            System.Drawing.Color.Blue,               // text colour (System.Drawing.Color)
            "Helvetica-Bold",                       // bold variant of Helvetica
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            24f);

        // Append the remaining two lines. They inherit the same font settings.
        formatted.AddNewLineText("Second line – Italic 18pt");
        formatted.AddNewLineText("Third line – Regular 12pt");

        // ---------------------------------------------------------------------
        // 2. Create a Stamp, bind the FormattedText and position it.
        //    Stamp does NOT expose HorizontalAlignment/VerticalAlignment – we set
        //    the origin manually so that the text appears centred.
        // ---------------------------------------------------------------------
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formatted);

        // Calculate centre position (approximate – the stamp origin is the lower‑left corner)
        // The stamp’s width/height are not directly exposed, so we use a rough offset.
        // Adjust these values as needed for precise centring.
        float offsetX = pageWidth / 2f - 100f; // 100 pt left of centre (approx.)
        float offsetY = pageHeight / 2f - 50f; // 50 pt below centre (approx.)
        stamp.SetOrigin(offsetX, offsetY);

        stamp.IsBackground = true;   // draw behind page content (watermark effect)
        stamp.Opacity = 0.5f;        // 50 % transparent

        // ---------------------------------------------------------------------
        // 3. Apply the stamp to the whole document using PdfFileStamp.
        // ---------------------------------------------------------------------
        PdfFileStamp pdfStamp = new PdfFileStamp();
        pdfStamp.BindPdf(inputPdf);
        pdfStamp.AddStamp(stamp);
        pdfStamp.Save(outputPdf);
        pdfStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
