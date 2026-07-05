using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the PdfFileMend facade on the loaded document
            PdfFileMend mend = new PdfFileMend(doc);

            // Configure word‑wrapping: enable wrapping and use the ByWords algorithm
            mend.IsWordWrap = true;                                 // enable wrapping
            mend.WrapMode   = WordWrapMode.ByWords;                 // wrap complete words only

            // Create the text to be added. FormattedText requires System.Drawing.Color.
            FormattedText ft = new FormattedText(
                "This is a very long line of text that will exceed the defined width and should be wrapped by words automatically by the PdfFileMend facade.",
                System.Drawing.Color.Black,                         // text color
                "Helvetica",                                        // font name
                EncodingType.Winansi,                               // encoding
                false,                                              // embed font?
                12);                                                // font size

            // Add the formatted text to page 1 at the specified coordinates.
            // With IsWordWrap = true and WrapMode = ByWords, the text will wrap within the available width.
            mend.AddText(ft, 1, 100, 700);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Wrapped text PDF saved to '{outputPdf}'.");
    }
}