using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

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

        // Create formatted text for the footer.
        // The constructor sets the text, color, font, encoding, embed flag and font size.
        // Word‑by‑word wrapping is performed automatically when the footer is added.
        FormattedText footer = new FormattedText(
            "This is a sample footer that will wrap word by word across the page width.",
            System.Drawing.Color.DarkGray,
            "Helvetica",
            EncodingType.Winansi,
            false,
            9);

        // Use PdfFileStamp facade to bind the source PDF, add the footer, and save the result.
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            stamp.BindPdf(inputPath);          // load the PDF
            stamp.AddFooter(footer, 15f);      // add footer with a 15‑point bottom margin
            stamp.Save(outputPath);            // write the output PDF
            stamp.Close();                     // finalize the operation
        }

        Console.WriteLine($"Footer added successfully. Output saved to '{outputPath}'.");
    }
}