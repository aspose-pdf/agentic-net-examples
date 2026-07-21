using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

class Program
{
    static void Main()
    {
        const string inputPdf  = "filled_form.pdf";   // PDF after AutoFiller has populated the form
        const string outputPdf = "watermarked_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF (Document implements IDisposable, so wrap in using)
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // At this point the form fields are already filled by AutoFiller.
            // Now add a text watermark to every page using PdfFileStamp.
            // -----------------------------------------------------------------

            // Initialize the PdfFileStamp facade on the existing Document.
            // PdfFileStamp does NOT implement IDisposable; do NOT wrap it in a using block.
            PdfFileStamp fileStamp = new PdfFileStamp(doc);

            // Create a Stamp object. Fully qualify to avoid ambiguity with Aspose.Pdf.Stamp.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Prepare the watermark text. FormattedText uses System.Drawing.Color for color.
            // Example: semi‑transparent gray text "CONFIDENTIAL".
            FormattedText ft = new FormattedText(
                "CONFIDENTIAL",                     // text
                System.Drawing.Color.FromArgb(128, 128, 128, 128), // semi‑transparent gray (ARGB)
                "Helvetica",                        // font name
                EncodingType.Winansi,               // encoding
                false,                              // embed font
                48);                                // font size

            // Bind the formatted text to the stamp.
            stamp.BindLogo(ft);

            // Place the stamp behind the page content (background).
            stamp.IsBackground = true;

            // Optionally set the position and size of the watermark.
            // Here we center it and let Aspose calculate the size.
            stamp.SetOrigin(100, 400);   // X, Y coordinates (adjust as needed)
            stamp.SetImageSize(300, 100); // Width, Height (adjust as needed)

            // Add the stamp to all pages.
            fileStamp.AddStamp(stamp);

            // Close the facade to finalize stamping.
            fileStamp.Close();

            // Save the final PDF with the watermark.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}