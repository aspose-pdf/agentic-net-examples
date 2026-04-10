using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the template PDF and the final output PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "output_with_date_header.pdf";

        // Ensure the template file exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Create a formatted text containing the current date.
        // PageDate provides a formatted date string; we use its default format.
        string dateString = new PageDate().GetFormattedDate();

        // FormattedText constructor requires System.Drawing.Color for the text color.
        FormattedText headerText = new FormattedText(
            dateString,                     // text to display
            System.Drawing.Color.Black,     // text color
            "Helvetica",                    // font name
            EncodingType.Winansi,           // encoding
            false,                          // embed font flag
            12);                            // font size

        // Use PdfFileStamp to add the header to every page of the document.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = templatePath;   // source PDF
        fileStamp.OutputFile = outputPath;    // destination PDF

        // Add the header with a top margin of 20 points.
        fileStamp.AddHeader(headerText, 20f);

        // Finalize and close the stamp operation.
        fileStamp.Close();

        Console.WriteLine($"PDF generated with date header: {outputPath}");
    }
}