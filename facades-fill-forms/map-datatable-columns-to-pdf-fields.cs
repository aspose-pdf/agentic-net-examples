using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // -----------------------------------------------------------------
        // 1. Prepare a DataTable (in real scenarios this would come from DB)
        // -----------------------------------------------------------------
        DataTable dataTable = new DataTable("Data");
        dataTable.Columns.Add("Company", typeof(string));
        dataTable.Columns.Add("Contact", typeof(string));
        dataTable.Columns.Add("Addr",    typeof(string));
        dataTable.Rows.Add("Acme Corp", "John Doe", "123 Main St");

        // ---------------------------------------------------------------
        // 2. Define a mapping from DataTable column names to PDF field names
        // ---------------------------------------------------------------
        var columnToFieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Company", "CompanyName" },
            { "Contact", "ContactName" },
            { "Addr",    "Address" }
        };

        // ---------------------------------------------------------------
        // 3. Ensure a template PDF exists – create a minimal one if missing
        // ---------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            // Create a simple PDF with the required form fields
            var doc = new Document();
            var page = doc.Pages.Add();
            foreach (var kvp in columnToFieldMap)
            {
                // Use the layout Rectangle (Aspose.Pdf.Rectangle) for field position
                var txtField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700 - 30 * doc.Form.Count, 200, 20))
                {
                    PartialName = kvp.Value,
                    Value = string.Empty
                };
                doc.Form.Add(txtField);
            }
            doc.Save(templatePath);
        }

        // ---------------------------------------------------------------
        // 4. Rename DataTable columns so they match the PDF field identifiers
        // ---------------------------------------------------------------
        foreach (DataColumn column in dataTable.Columns)
        {
            if (columnToFieldMap.TryGetValue(column.ColumnName, out string? pdfFieldName) &&
                !string.IsNullOrEmpty(pdfFieldName))
            {
                column.ColumnName = pdfFieldName; // rename column to match PDF field
            }
        }

        // ---------------------------------------------------------------
        // 5. (Optional) Verify that the renamed columns exist in the PDF
        // ---------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form(templatePath))
        {
            var pdfFields = new HashSet<string>(pdfForm.FieldNames, StringComparer.Ordinal);
            foreach (DataColumn column in dataTable.Columns)
            {
                if (!pdfFields.Contains(column.ColumnName))
                {
                    Console.Error.WriteLine($"Warning: PDF does not contain field '{column.ColumnName}'.");
                }
            }
        }

        // ---------------------------------------------------------------
        // 6. Fill the PDF using AutoFiller and save the result
        // ---------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);   // bind the template PDF
            autoFiller.ImportDataTable(dataTable); // import the adjusted DataTable
            autoFiller.Save(outputPath);        // save the filled PDF
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}
