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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfFileStamp facade with the loaded document
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(doc);

            // Create a stamp that will act as a rotated text annotation
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // FormattedText constructor requires System.Drawing.Color (not Aspose.Pdf.Color)
            FormattedText formatted = new FormattedText(
                "Rotated Text",                     // text content
                System.Drawing.Color.Blue,          // text color
                "Helvetica",                        // font name
                EncodingType.Winansi,               // encoding
                false,                              // embed font flag
                24);                                // font size

            // Bind the formatted text to the stamp
            stamp.BindLogo(formatted);

            // Rotate the stamp by 90 degrees
            stamp.Rotation = 90f;

            // Add the stamp (rotated text annotation) to the PDF
            fileStamp.AddStamp(stamp);

            // Save the modified PDF
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Rotated text annotation saved to '{outputPath}'.");
    }
}