using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Specify the pages that should receive the stamp (1‑based indexing)
        int[] pagesToStamp = new int[] { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Initialize the facade with the loaded document
            PdfFileStamp fileStamp = new PdfFileStamp(doc);

            // Create the formatted text that will appear as the stamp
            // Parameters: text, color (System.Drawing.Color), font name, encoding, embedded flag, font size
            Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
                "CONFIDENTIAL",
                System.Drawing.Color.Red,
                "Helvetica",
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,
                48);

            // Configure the stamp
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(formattedText);      // bind the text to the stamp
            stamp.IsBackground = true;          // place the stamp behind page content
            stamp.Opacity = 0.7f;               // 70% opacity
            stamp.Pages = pagesToStamp;         // apply only to selected pages

            // Add the stamp to the document and save
            fileStamp.AddStamp(stamp);
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}