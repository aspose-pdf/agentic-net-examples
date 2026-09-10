using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color used by FormattedText
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);

            // Create a stamp (fully qualified to avoid ambiguity)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind simple text as the stamp appearance using System.Drawing.Color and a float font size
            Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                "Diagonal Stamp",          // text
                System.Drawing.Color.Gray, // text color (System.Drawing.Color)
                "Helvetica",               // font name
                Aspose.Pdf.Facades.EncodingType.Winansi, // encoding
                false,                      // embed font?
                36f);                       // font size (float)

            stamp.BindLogo(ft);

            // Set rotation to 45 degrees for diagonal placement
            stamp.Rotation = 45f;

            // Add the stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the result
            fileStamp.Save(outputPdf);
        }

        Console.WriteLine($"Stamp with 45° rotation applied and saved to '{outputPdf}'.");
    }
}
