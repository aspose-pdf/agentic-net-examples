using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Dynamic author name (could be retrieved from elsewhere)
        const string author = "John Doe";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Load the PDF using the Facade API (PdfFileStamp)
        // -----------------------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // -----------------------------------------------------------------
        // Build the stamp text with string interpolation (date + author)
        // -----------------------------------------------------------------
        string stampText = $"Generated on {DateTime.Now:yyyy-MM-dd HH:mm} by {author}";

        // FormattedText constructor requires System.Drawing.Color for the text color
        Aspose.Pdf.Facades.FormattedText formatted = new Aspose.Pdf.Facades.FormattedText(
            stampText,                                 // text
            System.Drawing.Color.DarkGray,             // text color
            "Helvetica",                               // font name
            Aspose.Pdf.Facades.EncodingType.Winansi,   // encoding
            false,                                     // isEmbedded (false = use system font)
            10);                                       // font size

        // -----------------------------------------------------------------
        // Create a Stamp, bind the formatted text, and set its position
        // -----------------------------------------------------------------
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formatted);          // attach the text to the stamp
        stamp.SetOrigin(100f, 100f);        // X and Y coordinates on the page
        stamp.IsBackground = false;        // render on top of page content

        // -----------------------------------------------------------------
        // Add the stamp to the document (applies to all pages by default)
        // -----------------------------------------------------------------
        fileStamp.AddStamp(stamp);

        // -----------------------------------------------------------------
        // Save the stamped PDF and release resources
        // -----------------------------------------------------------------
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}