using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampText = "CONFIDENTIAL";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp that contains formatted text
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        FormattedText formatted = new FormattedText(
            stampText,                     // text to display
            System.Drawing.Color.Red,      // text color (System.Drawing.Color is required here)
            "Helvetica",                   // font name
            EncodingType.Winansi,          // encoding
            false,                         // embed font flag
            36);                           // font size

        stamp.BindLogo(formatted);          // bind the formatted text to the stamp
        stamp.SetOrigin(100, 700);          // position of the stamp on the page
        stamp.Opacity = 0.5f;               // semi‑transparent
        stamp.IsBackground = true;          // render behind existing content

        // Apply the stamp only to pages 1, 5, and 10
        stamp.Pages = new int[] { 1, 5, 10 };

        // Add the stamp to the document and save the result
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamp applied to pages 1, 5, and 10. Saved as '{outputPdf}'.");
    }
}