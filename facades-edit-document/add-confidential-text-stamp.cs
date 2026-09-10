using System;
using System.IO;
using System.Drawing;                     // needed for System.Drawing.Color in FormattedText
using Aspose.Pdf;                         // core PDF classes
using Aspose.Pdf.Facades;                 // facades for stamping and formatted text
using Aspose.Pdf.Text;                    // for TextStamp if needed (not used here)

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a formatted text object with the desired appearance:
        // - Text: "Confidential"
        // - Red font color (System.Drawing.Color)
        // - Helvetica font, 48 pt size
        // - Winansi encoding, not embedded
        Aspose.Pdf.Facades.FormattedText formatted = new Aspose.Pdf.Facades.FormattedText(
            "Confidential",
            System.Drawing.Color.Red,               // text color (System.Drawing.Color)
            "Helvetica",                           // font name
            Aspose.Pdf.Facades.EncodingType.Winansi, // encoding
            false,                                   // embed font?
            48f);                                    // font size (float)

        // Create a stamp object and bind the formatted text to it
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formatted);

        // Set the stamp to appear as a background element (behind page content)
        stamp.IsBackground = true;

        // Make the stamp semi‑transparent (opacity range 0.0 to 1.0)
        stamp.Opacity = 0.5f;

        // Position the stamp on the page (e.g., centered). Adjust as needed.
        stamp.SetOrigin(100, 500);

        // Use PdfFileStamp facade to apply the stamp to all pages
        using (Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp())
        {
            // Bind the source PDF
            fileStamp.BindPdf(inputPdf);

            // Add the prepared stamp; it will be applied to every page
            fileStamp.AddStamp(stamp);

            // Save the result to the output file
            fileStamp.Save(outputPdf);

            // Close the facade (releases resources)
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
