using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for TextAbsorber
using System.Drawing; // required for System.Drawing.Color

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "stamped_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Add a rotated text stamp ----------
        // Bind the source PDF to the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a text stamp using FormattedText (System.Drawing.Color is required)
        FormattedText ft = new FormattedText(
            "Rotated Stamp",          // text
            System.Drawing.Color.Black, // text color (System.Drawing.Color)
            "Helvetica",               // font name
            EncodingType.Winansi,       // encoding
            false,                      // embed font?
            36f);                       // font size (float)

        // Fully qualify the Stamp class from Aspose.Pdf.Facades to avoid ambiguity
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(ft);          // set the text as the stamp content
        stamp.Rotation = 90;         // rotate 90 degrees (clockwise)

        // Add the stamp to all pages (null means all pages)
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdf);   // persist changes
        fileStamp.Close();           // close the facade

        // ---------- Verify that the stamped text is still readable ----------
        // Load the resulting PDF and extract all text
        using (Document doc = new Document(outputPdf))
        {
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text;

            if (string.IsNullOrWhiteSpace(extractedText))
            {
                Console.WriteLine("Verification failed: No text extracted from the stamped PDF.");
            }
            else
            {
                Console.WriteLine($"Verification succeeded: Extracted text length = {extractedText.Length}");
                // Optionally display a snippet of the extracted text
                Console.WriteLine("Sample extracted text: " +
                    (extractedText.Length > 100 ? extractedText.Substring(0, 100) + "..." : extractedText));
            }
        }
    }
}
