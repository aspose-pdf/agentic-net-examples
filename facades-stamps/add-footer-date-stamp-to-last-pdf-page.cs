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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine the last page number of the source PDF.
        int lastPageNumber;
        using (Document doc = new Document(inputPath))
        {
            lastPageNumber = doc.Pages.Count; // 1‑based indexing
        }

        // Create a formatted text containing the current date (MM-dd-yyyy).
        FormattedText dateText = new FormattedText(
            DateTime.Now.ToString("MM-dd-yyyy"),   // text
            System.Drawing.Color.Black,            // text color (System.Drawing.Color required)
            "Helvetica",                           // font name
            EncodingType.Winansi,                  // encoding
            false,                                 // embed font?
            12);                                   // font size

        // Prepare a stamp that will place the formatted text.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(dateText);                 // bind the text to the stamp
        stamp.Pages = new int[] { lastPageNumber }; // apply only to the last page

        // Configure the PdfFileStamp facade.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPath;
        fileStamp.OutputFile = outputPath;

        // Add the stamp and finalize.
        fileStamp.AddStamp(stamp);
        fileStamp.Close(); // saves the result

        Console.WriteLine($"Footer with date added to last page. Saved as '{outputPath}'.");
    }
}