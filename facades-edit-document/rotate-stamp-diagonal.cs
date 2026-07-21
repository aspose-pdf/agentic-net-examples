using System;
using System.IO;
using System.Drawing; // Required for FormattedText color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfFileStamp facade with the loaded document
            PdfFileStamp fileStamp = new PdfFileStamp(doc);

            // Create a stamp (facade) and bind text to it
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                "DIAGONAL",                     // Text to display
                System.Drawing.Color.Red,        // Text color (System.Drawing.Color)
                "Helvetica",                    // Font name
                Aspose.Pdf.Facades.EncodingType.Winansi, // Encoding
                false,                           // Is embedded
                48f);                            // Font size (float)

            stamp.BindLogo(ft);

            // Apply a 45‑degree rotation for diagonal placement
            stamp.Rotation = 45f;

            // Add the configured stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the result and release resources
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamp applied with 45° rotation and saved to '{outputPath}'.");
    }
}
