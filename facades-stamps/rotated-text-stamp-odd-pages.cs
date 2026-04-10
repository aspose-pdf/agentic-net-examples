using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.InputFile = inputPath;
            fileStamp.OutputFile = outputPath;
            fileStamp.BindPdf(inputPath);

            // Determine all odd‑numbered pages (Aspose.Pdf uses 1‑based indexing)
            int pageCount = fileStamp.Document.Pages.Count;
            List<int> oddPages = new List<int>();
            for (int i = 1; i <= pageCount; i += 2)
                oddPages.Add(i);

            // Create a formatted text object for the stamp
            FormattedText txt = new FormattedText(
                "Rotated Stamp",                     // text
                System.Drawing.Color.Red,            // text color (System.Drawing)
                "Helvetica",                         // font name
                EncodingType.Winansi,                // encoding
                false,                               // embed font?
                24);                                 // font size

            // Configure the stamp
            Stamp stamp = new Stamp();
            stamp.BindLogo(txt);                     // use text as stamp content
            stamp.Rotation = 30f;                    // rotate 30 degrees
            stamp.SetOrigin(0f, 500f);               // place near left margin (X=0), Y=500
            stamp.Pages = oddPages.ToArray();        // apply only to odd pages

            // Add the stamp to the document
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}