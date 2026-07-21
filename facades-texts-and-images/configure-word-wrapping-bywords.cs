using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Color required by FormattedText
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // PdfFileMend, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string outputPath = "wrapped_text.pdf";

        // Create a new PDF document with a single blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add(); // page index will be 1 (1‑based)

            // Initialize the PdfFileMend facade on the document
            using (PdfFileMend mend = new PdfFileMend())
            {
                mend.BindPdf(doc);                     // bind the facade to the document
                mend.IsWordWrap = true;                // enable word‑wrap handling
                mend.WrapMode = WordWrapMode.ByWords;  // set wrap algorithm to ByWords

                // Prepare the text to be added. The constructor requires System.Drawing.Color.
                Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                    "This is a very long line of text that should automatically wrap by complete words within the defined rectangle width.",
                    System.Drawing.Color.Black,          // text color (System.Drawing.Color)
                    "Helvetica",                         // font name
                    Aspose.Pdf.Facades.EncodingType.Winansi, // encoding
                    false,                                 // embed font flag
                    12f);                                   // font size (float)

                // Add the text to page 1.
                // Parameters: FormattedText, pageNumber, lowerLeftX, lowerLeftY, width, height
                // Width defines the bounding box for wrapping; height can be 0 if not needed.
                mend.AddText(ft, 1, 100f, 500f, 200f, 0f);
                
                // Save the modified document to the output file
                mend.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
