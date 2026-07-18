using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (required by the lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfFileStamp with input and output files
            // PdfFileStamp does not implement IDisposable, so we close it manually
            PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);

            // Create a text stamp (acts as a text annotation)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind the text to the stamp using FormattedText.
            // Note: FormattedText uses System.Drawing.Color for the text color.
            FormattedText ft = new FormattedText(
                "Diagonal Text",                 // text
                System.Drawing.Color.Black,      // text color
                "Helvetica",                     // font name
                EncodingType.Winansi,            // encoding
                false,                           // embed font?
                12);                             // font size

            stamp.BindLogo(ft);

            // Rotate the stamp 90 degrees to align with diagonal content
            stamp.Rotation = 90f;

            // Optionally set the position of the stamp on the page
            // (origin is the lower‑left corner of the page)
            stamp.SetOrigin(100, 500);

            // Add the stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Finalize and save the output PDF
            fileStamp.Close();
        }

        Console.WriteLine($"Rotated text annotation saved to '{outputPath}'.");
    }
}