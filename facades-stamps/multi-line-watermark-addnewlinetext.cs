using System;
using System.Drawing; // Required for System.Drawing.Color used by FormattedText
using Aspose.Pdf.Facades; // Facade classes for stamping

class Program
{
    static void Main()
    {
        // Input PDF to which the watermark will be applied
        const string inputPdf = "input.pdf";
        // Output PDF with the watermark
        const string outputPdf = "watermarked.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Create a PdfFileStamp facade (do NOT wrap in a using block – it does
        // not implement IDisposable). Bind the source PDF file.
        // -----------------------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // -----------------------------------------------------------------
        // Build a FormattedText object that contains three lines.
        // Each line is added via AddNewLineText. The constructor sets the
        // style for the first line; additional lines inherit the same style.
        // To apply different styles per line you would create separate stamps,
        // but this example demonstrates the required AddNewLineText usage.
        // -----------------------------------------------------------------
        FormattedText formattedText = new FormattedText(
            "First line of watermark",          // first line text
            Color.Red,                         // text color (System.Drawing.Color)
            "Helvetica",                       // font name
            EncodingType.Winansi,              // encoding
            false,                             // embed font flag
            36);                               // font size

        // Add the remaining two lines
        formattedText.AddNewLineText("Second line with same style");
        formattedText.AddNewLineText("Third line with same style");

        // -----------------------------------------------------------------
        // Create a generic Stamp, bind the FormattedText to it, and set
        // positioning and visual properties.
        // -----------------------------------------------------------------
        Stamp stamp = new Stamp();
        stamp.BindLogo(formattedText);   // Attach the text to the stamp
        stamp.SetOrigin(100, 500);       // Position (X, Y) on the page
        stamp.IsBackground = true;      // Render behind existing content
        stamp.Opacity = 0.5f;            // Semi‑transparent

        // Add the stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Save the result and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}