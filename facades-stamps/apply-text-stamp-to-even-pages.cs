using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_even_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to determine the number of pages.
        using (Document doc = new Document(inputPath))
        {
            // Collect even page numbers (Aspose.Pdf uses 1‑based indexing).
            List<int> evenPages = new List<int>();
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (i % 2 == 0) // parity check
                    evenPages.Add(i);
            }

            // Initialize the facade and bind the source PDF.
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPath);

            // Create a text stamp (you can replace this with an image stamp if desired).
            // Fully qualify the Stamp class to avoid ambiguity.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Prepare formatted text for the stamp.
            // FormattedText constructor uses System.Drawing.Color for the text color.
            FormattedText ft = new FormattedText(
                "EVEN PAGE STAMP",               // text
                System.Drawing.Color.Red,        // text color
                "Helvetica",                     // font name
                EncodingType.Winansi,            // encoding
                false,                           // embed font?
                36);                             // font size

            // Bind the formatted text to the stamp.
            stamp.BindLogo(ft);

            // Position the stamp (origin coordinates in points).
            stamp.SetOrigin(100, 500);
            stamp.IsBackground = false; // place on top of page content
            stamp.Opacity = 0.5f;

            // Apply the stamp only to the even pages collected above.
            stamp.Pages = evenPages.ToArray();

            // Add the stamp to the document via the facade.
            fileStamp.AddStamp(stamp);

            // Save the result.
            fileStamp.Save(outputPath);
            fileStamp.Close();

            Console.WriteLine($"Stamp applied to even pages. Output saved to '{outputPath}'.");
        }
    }
}