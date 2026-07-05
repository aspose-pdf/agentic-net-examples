using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form_template.pdf";   // PDF containing the form
        const string outputPdfPath = "form_report.pdf";    // PDF report to generate

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF and bind it to a FormEditor facade
        using (Document srcDoc = new Document(inputPdfPath))
        using (FormEditor formEditor = new FormEditor(srcDoc))
        {
            // Add a new page at the end of the document for the report
            Page reportPage = srcDoc.Pages.Add();

            // Create a table with three columns: Name, Type, Value
            Table table = new Table
            {
                // Optional: set column widths (percentage of page width)
                ColumnWidths = "30 30 40"
            };

            // Add header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Color.LightGray;
            header.Cells.Add("Field Name");
            header.Cells.Add("Field Type");
            header.Cells.Add("Field Value");

            // Iterate over each form field and populate the table
            foreach (Field field in srcDoc.Form.Fields)
            {
                string fieldName = field.PartialName;
                string fieldType = field.GetType().Name; // e.g., TextBoxField, CheckBoxField, etc.
                string fieldValue = field.Value?.ToString() ?? string.Empty;

                Row row = table.Rows.Add();
                row.Cells.Add(fieldName);
                row.Cells.Add(fieldType);
                row.Cells.Add(fieldValue);
            }

            // Add the table to the report page
            reportPage.Paragraphs.Add(table);

            // Save the resulting PDF (inside the using block to keep the document alive)
            srcDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form report generated: {outputPdfPath}");
    }
}
