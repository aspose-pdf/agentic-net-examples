using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the template PDF and the final output PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "output_with_date_header.pdf";

        // Ensure the template file exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // 1. Use AutoFiller to generate a PDF from the template.
        //    In this example no data table is imported; the template is simply copied.
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the template PDF file
            autoFiller.BindPdf(templatePath);

            // Save the generated PDF into a memory stream for further processing
            using (MemoryStream generatedPdf = new MemoryStream())
            {
                autoFiller.Save(generatedPdf);
                generatedPdf.Position = 0; // Reset stream position for reading

                // 2. Add a header with the current date to each page using PdfFileStamp.
                using (PdfFileStamp pdfStamp = new PdfFileStamp())
                {
                    // Bind the generated PDF stream
                    pdfStamp.BindPdf(generatedPdf);

                    // Prepare the header text (current date) as FormattedText.
                    // FormattedText constructor requires System.Drawing.Color for the text color.
                    string currentDate = DateTime.Now.ToString("D"); // e.g., "Monday, 15 April 2026"
                    var headerText = new FormattedText(
                        currentDate,                     // Text to display
                        System.Drawing.Color.Black,      // Text color
                        "Helvetica",                     // Font name
                        EncodingType.Winansi,            // Encoding
                        false,                           // Do not embed the font
                        12);                             // Font size

                    // Add the header to all pages. The second argument is the top margin (in points).
                    pdfStamp.AddHeader(headerText, 20f);

                    // Save the final PDF with the header.
                    pdfStamp.Save(outputPath);
                }
            }
        }

        Console.WriteLine($"PDF generated with date header: {outputPath}");
    }
}