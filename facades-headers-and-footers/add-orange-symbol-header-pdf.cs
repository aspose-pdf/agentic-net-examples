using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into a Document first (PdfFileStamp expects a Document instance)
        Document doc = new Document(inputPath);

        // Use PdfFileStamp facade to modify the document
        using (PdfFileStamp stamp = new PdfFileStamp(doc))
        {
            // Use the FormattedText overload that accepts a font name (string).
            // System.Drawing.Color is explicitly qualified to avoid ambiguity with Aspose.Pdf.Color.
            FormattedText header = new FormattedText(
                "Symbol",                     // text
                System.Drawing.Color.Orange,   // text color (System.Drawing.Color)
                "Helvetica",                  // font name
                EncodingType.Winansi,          // encoding
                false,                         // isBold
                12f);                          // font size

            // Add the header with a top margin of 7 points
            stamp.AddHeader(header, 7);

            // Save the modified PDF
            stamp.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
