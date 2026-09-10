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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfFileStamp facade with the loaded document
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(doc);

            // Create a text stamp
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // FormattedText requires System.Drawing.Color for the text color
            FormattedText ft = new FormattedText(
                "Rotated Text",                 // text
                System.Drawing.Color.Black,     // text color
                "Helvetica",                    // font name
                EncodingType.Winansi,           // encoding
                false,                          // embed font?
                12);                            // font size

            stamp.BindLogo(ft);

            // Rotate the stamp 90 degrees
            stamp.Rotation = 90f;

            // Add the stamp to the document (default page is the first page)
            fileStamp.AddStamp(stamp);

            // Save the modified PDF
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Rotated text annotation saved to '{outputPath}'.");
    }
}