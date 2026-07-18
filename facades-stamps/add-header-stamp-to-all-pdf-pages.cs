using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade API for stamping
using Aspose.Pdf.Text;            // FormattedText and EncodingType
using System.Drawing;             // System.Drawing.Color required by FormattedText

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string companyName = "Acme Corporation";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Load the document only to obtain the page count (required for iteration)
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            int pageCount = doc.Pages.Count; // 1‑based indexing

            // Iterate through each page and add a header stamp with the company name
            for (int i = 1; i <= pageCount; i++)
            {
                // Create a formatted text object for the header
                FormattedText headerText = new FormattedText(
                    companyName,                 // text
                    Color.Black,                 // text color (System.Drawing.Color)
                    "Helvetica",                 // font name
                    EncodingType.Winansi,        // encoding
                    false,                       // embed font?
                    12);                         // font size

                // Create a stamp and bind the formatted text
                Stamp stamp = new Stamp();
                stamp.BindLogo(headerText);

                // Configure stamp appearance (optional)
                stamp.IsBackground = false;   // place on top of page content
                stamp.Opacity = 1.0f;         // fully opaque

                // Restrict the stamp to the current page only
                stamp.Pages = new int[] { i };

                // Add the stamp to the file
                fileStamp.AddStamp(stamp);
            }
        }

        // Save the stamped PDF and release resources
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Header stamp added to all pages. Output saved to '{outputPath}'.");
    }
}