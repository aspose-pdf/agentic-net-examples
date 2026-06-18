using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade classes: PdfFileStamp, Stamp, FormattedText, EncodingType
using Aspose.Pdf;                 // Core classes (if needed for other operations)

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PdfFileStamp facade and bind the source PDF
        using (PdfFileStamp pdfStamp = new PdfFileStamp())
        {
            pdfStamp.BindPdf(inputPdf);   // Load the document to be stamped

            // Build the formatted text for the watermark.
            // The constructor parameters are:
            //   text, color (System.Drawing.Color), font name, encoding, isBold, font size
            var formattedText = new Aspose.Pdf.Facades.FormattedText(
                "Confidential",                         // First line (initial text)
                System.Drawing.Color.Red,               // Text color
                "Helvetica",                            // Font name
                Aspose.Pdf.Facades.EncodingType.Winansi, // Encoding
                false,                                  // Not bold
                36);                                    // Font size (points)

            // Append additional lines with explicit line spacing.
            // The second argument is the extra spacing added after the line.
            formattedText.AddNewLineText("Do Not Distribute", 12f); // 12 points extra spacing
            formattedText.AddNewLineText("Company XYZ", 12f);       // 12 points extra spacing

            // Create a Stamp object and bind the formatted text to it.
            var stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(formattedText);   // Use the text as the stamp content

            // Optional visual settings for the stamp
            stamp.IsBackground = true;      // Render behind page content
            stamp.Opacity      = 0.3f;      // Semi‑transparent
            stamp.SetOrigin(100f, 400f);    // Position (X, Y) from the lower‑left corner

            // Add the stamp to all pages of the document
            pdfStamp.AddStamp(stamp);

            // Save the result and release resources
            pdfStamp.Save(outputPdf);
            pdfStamp.Close();
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}