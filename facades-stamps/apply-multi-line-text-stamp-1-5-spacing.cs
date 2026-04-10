using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create formatted text with multiple lines.
        // The constructor sets the first line; additional lines are added with line spacing.
        Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
            "Confidential",                     // first line
            System.Drawing.Color.Red,           // text color
            "Helvetica",                        // font name
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,                              // embed font
            36);                                // font size

        // Append extra lines; the second parameter is additional line spacing.
        formattedText.AddNewLineText("Do Not Distribute", 1.5f);
        formattedText.AddNewLineText("Authorized Personnel Only", 1.5f);

        // Create a stamp and bind the formatted text to it.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formattedText);

        // Position the stamp (example coordinates).
        stamp.SetOrigin(100f, 400f);

        // Render the stamp as a background element with semi‑transparent opacity.
        stamp.IsBackground = true;
        stamp.Opacity = 0.5f;

        // Apply the stamp to all pages of the document.
        fileStamp.AddStamp(stamp);

        // Save the watermarked PDF.
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}