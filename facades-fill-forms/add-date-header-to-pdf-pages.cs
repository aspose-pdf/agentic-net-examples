using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string intermediatePath = "filled.pdf";
        const string finalPath = "final_with_header.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Fill the template (no data table needed for header only)
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePath);
            // If you have a DataTable, import it here:
            // filler.ImportDataTable(myDataTable);
            filler.Save(intermediatePath);
        }

        // Prepare header text with current date
        string dateString = DateTime.Now.ToString("D"); // e.g., "Monday, 15 July 2026"
        FormattedText headerText = new FormattedText(
            dateString,
            System.Drawing.Color.Black, // text color (System.Drawing.Color is required)
            "Helvetica",                // font name
            EncodingType.Winansi,       // encoding
            false,                      // embedded flag
            12);                        // font size

        // Add the header to each page of the filled PDF
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            stamp.BindPdf(intermediatePath);
            stamp.AddHeader(headerText, 20); // 20 points top margin
            stamp.Save(finalPath);
            stamp.Close();
        }

        Console.WriteLine($"Generated PDF with date header: {finalPath}");
    }
}