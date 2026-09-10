using System;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // <-- required for Border and BorderStyle

class Program
{
    static void Main()
    {
        // Paths for the template and the final output PDF
        const string templatePath = "template.pdf";
        const string generatedPath = "generated.pdf"; // intermediate file without header
        const string outputPath   = "output_with_header.pdf";

        // --------------------------------------------------------------------
        // 1. Ensure a template PDF exists (create it inline if missing)
        // --------------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            // Create a simple template containing a TextBoxField whose PartialName
            // matches the column name we will use in the DataTable ("SampleField").
            using (Document templateDoc = new Document())
            {
                Page page = templateDoc.Pages.Add();
                // Position the field somewhere on the page
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
                // Initialise the field first
                TextBoxField txtField = new TextBoxField(page, rect)
                {
                    PartialName = "SampleField",
                    Value = string.Empty,
                    Color = Aspose.Pdf.Color.Black
                };
                // Now set the border – note that we must reference the already created txtField
                txtField.Border = new Border(txtField)
                {
                    Style = BorderStyle.Solid,
                    Width = 1
                };
                // Add the field to the form
                templateDoc.Form.Add(txtField);
                templateDoc.Save(templatePath);
            }
        }

        // --------------------------------------------------------------------
        // 2. Generate PDF pages using AutoFiller
        // --------------------------------------------------------------------
        // Create a simple DataTable matching the fields in the template.
        DataTable data = new DataTable();
        data.Columns.Add("SampleField", typeof(string));
        data.Rows.Add("SampleValue");

        // AutoFiller creates the merged PDF from the template and data.
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);   // Bind the template PDF
            autoFiller.ImportDataTable(data);   // Import data rows
            autoFiller.Save(generatedPath);     // Save the generated PDF (without header)
        }

        // --------------------------------------------------------------------
        // 3. Add a header with the current date to each page
        // --------------------------------------------------------------------
        using (PdfFileStamp pdfStamp = new PdfFileStamp())
        {
            // Initialize the facade with the PDF produced by AutoFiller
            pdfStamp.BindPdf(generatedPath);

            // Prepare the header text (full date string)
            string dateHeader = DateTime.Now.ToString("D"); // e.g., "Monday, 18 August 2026"

            // FormattedText requires System.Drawing.Color for the text color.
            FormattedText formattedHeader = new FormattedText(
                dateHeader,                     // Text to display
                System.Drawing.Color.Black,    // Text color (System.Drawing)
                "Helvetica",                  // Font name
                EncodingType.Winansi,          // Encoding
                false,                         // Is embedded?
                12);                           // Font size

            // Add the header at a top margin of 20 points.
            pdfStamp.AddHeader(formattedHeader, 20);

            // Persist changes to the final output file.
            pdfStamp.Save(outputPath);
        }

        Console.WriteLine("PDF generated with a date header on each page.");
    }
}
