using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Bind the template PDF to AutoFiller (no data import needed for this task)
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.Close(); // release resources

        // Create a stamp that will add a header with the current date on every page
        PdfFileStamp stamp = new PdfFileStamp();
        stamp.BindPdf(templatePath);

        // Build the header text (full date string)
        string dateHeader = DateTime.Now.ToString("D"); // e.g., "Monday, 04 July 2026"

        // FormattedText requires System.Drawing.Color for the text color
        FormattedText headerText = new FormattedText(
            dateHeader,                     // text
            System.Drawing.Color.Black,     // text color
            "Helvetica",                    // font name
            EncodingType.Winansi,           // encoding
            false,                          // embed font
            12);                            // font size

        // Add the header at a top margin of 20 points
        stamp.AddHeader(headerText, 20);

        // Save the resulting PDF
        stamp.Save(outputPath);
        stamp.Close();

        Console.WriteLine($"PDF with date header saved to '{outputPath}'.");
    }
}