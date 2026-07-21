using System;
using System.IO;
using Aspose.Pdf.Facades;               // Facade APIs for stamping
using Aspose.Pdf;                       // Core PDF types (Document, Page, etc.)
using Aspose.Pdf.Text;                  // For FormattedText (requires System.Drawing.Color)

// Apply a text stamp only on pages 1, 5 and 10 of an existing PDF.
class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "stamped_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF file.
            fileStamp.BindPdf(inputPdf);

            // Create a text stamp using FormattedText.
            // Note: FormattedText constructor requires System.Drawing.Color.
            FormattedText ft = new FormattedText(
                "CONFIDENTIAL",                     // Text to display
                System.Drawing.Color.Red,           // Text color
                "Helvetica",                        // Font name
                EncodingType.Winansi,               // Encoding
                false,                              // Embedded flag
                36);                                // Font size

            // Create the stamp object (fully qualified to avoid ambiguity).
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(ft);                    // Attach the formatted text.
            stamp.IsBackground = true;            // Render behind page content.
            stamp.Opacity = 0.5f;                  // Semi‑transparent.

            // Restrict the stamp to pages 1, 5 and 10.
            stamp.Pages = new int[] { 1, 5, 10 };

            // Add the configured stamp to the PDF.
            fileStamp.AddStamp(stamp);

            // Save the result. The Save method writes the output file.
            fileStamp.Save(outputPdf);

            // Close releases resources (optional because of using).
            fileStamp.Close();
        }

        Console.WriteLine($"Stamp applied to pages 1, 5, 10. Output saved as '{outputPdf}'.");
    }
}