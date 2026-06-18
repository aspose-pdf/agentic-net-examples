using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // FormattedText and EncodingType are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "rotated_stamp.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the PdfFileStamp facade and bind it to the loaded document
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(doc);

            // Create formatted text that will be used as the stamp content
            FormattedText formattedText = new FormattedText(
                "ROTATED TEXT",                     // text to display
                System.Drawing.Color.Black,         // text color (System.Drawing.Color is required here)
                "Helvetica",                        // font name
                EncodingType.Winansi,               // encoding
                false,                              // embed font flag
                36);                                // font size

            // Create a stamp, bind the formatted text, and set rotation to 90 degrees
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(formattedText);
            stamp.Rotation = 90; // rotate the stamp 90° clockwise

            // Add the stamp to all pages (Pages = null means all pages)
            fileStamp.AddStamp(stamp);

            // Save the modified PDF to the output path
            fileStamp.Save(outputPdf);
            fileStamp.Close(); // close the facade to release resources
        }

        // Simple verification output
        Console.WriteLine($"Aspose.Pdf.Facades.Stamp with 90° rotation added. Output saved to '{outputPdf}'.");
    }
}