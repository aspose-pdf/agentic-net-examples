using System;
using System.IO;
using System.Drawing;                     // Required for System.Drawing.Color used by FormattedText
using Aspose.Pdf;                         // Core PDF classes
using Aspose.Pdf.Facades;                 // PdfFileStamp, Aspose.Pdf.Facades.Stamp, FormattedText
using Aspose.Pdf.Text;                    // EncodingType enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the source PDF to the facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Build the date string in the required format
        string dateString = DateTime.Now.ToString("yyyy-MM-dd");

        // Create formatted text for the stamp (color, font, size)
        // NOTE: Use System.Drawing.Color for the color argument and a float for the font size
        FormattedText formatted = new FormattedText(
            dateString,                     // text to display
            System.Drawing.Color.Black,    // text color (System.Drawing.Color)
            "Helvetica",                  // font name
            EncodingType.Winansi,          // encoding
            false,                         // embed font? (false = use system font)
            12f);                          // font size (float)

        // Create the stamp and bind the formatted text
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formatted);

        // Position the stamp at the top‑left corner.
        // PdfFileStamp provides PageHeight (height of the first page).
        // Origin is measured from the lower‑left corner, so Y = PageHeight - offset.
        const float marginLeft = 10f;                     // left margin in points
        const float marginTop  = 20f;                     // distance from top edge
        float originY = fileStamp.PageHeight - marginTop; // Y coordinate from bottom
        stamp.SetOrigin(marginLeft, originY);

        // Ensure the stamp is drawn over the page content (default is false)
        stamp.IsBackground = false;

        // Add the stamp to the document
        fileStamp.AddStamp(stamp);

        // Save the result and release resources
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp added. Output saved to '{outputPath}'.");
    }
}
