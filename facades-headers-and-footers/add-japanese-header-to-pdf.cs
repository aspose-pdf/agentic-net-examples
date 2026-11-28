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

        // Load the PDF document (PdfFileStamp works with a Document instance)
        Document doc = new Document(inputPath);

        // Prepare formatted text for the header (Japanese characters "機密")
        // Fully qualify ambiguous types and use the correct FontStyle enum value.
        var header = new FormattedText(
            "機密",
            System.Drawing.Color.Black,
            Aspose.Pdf.Facades.FontStyle.Helvetica,
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            12f);

        // Apply the header stamp with a top margin of 10 points
        PdfFileStamp stamp = new PdfFileStamp(doc);
        stamp.AddHeader(header, 10f);
        stamp.Save(outputPath);

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}
