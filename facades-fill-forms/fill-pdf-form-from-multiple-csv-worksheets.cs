using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms; // <-- added namespace for form field types

class Program
{
    // Simple container for a sheet name and its associated DataTable
    private class SheetData
    {
        // Marked as required to satisfy the non‑nullable warning (C# 11+)
        public required string SheetName { get; set; }
        public required DataTable Table { get; set; }
    }

    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";
        const string outputDir = "FilledPdfs";

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------------
        // Instead of reading Excel worksheets (Aspose.Cells is not referenced),
        // we create a few in‑memory DataTable objects that mimic separate sheets.
        // ---------------------------------------------------------------------
        List<SheetData> sheets = GetSampleSheets();

        foreach (var sheet in sheets)
        {
            // Load the PDF template for each sheet – using a using‑block guarantees disposal.
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // Fill the PDF form fields using the DataTable for the current sheet.
                FillPdfForm(pdfDoc, sheet.Table);

                // Save the filled PDF; each sheet gets its own output file.
                string outputPath = Path.Combine(outputDir, $"filled_{sheet.SheetName}.pdf");
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Saved filled PDF for sheet '{sheet.SheetName}' to '{outputPath}'.");
            }
        }
    }

    /// <summary>
    /// Fills a PDF document's form fields using the first row of the supplied DataTable.
    /// Each column name is treated as the field's FullName. The method supports TextBoxField
    /// and generic fields (e.g., checkboxes) via reflection, matching the patterns recommended
    /// in the "Fix: use-document-form-fields-instead-of-autofiller" guidance.
    /// </summary>
    private static void FillPdfForm(Document pdfDoc, DataTable table)
    {
        if (table.Rows.Count == 0)
            return; // nothing to fill

        DataRow row = table.Rows[0]; // simple scenario – fill using the first row only

        foreach (DataColumn col in table.Columns)
        {
            string fieldName = col.ColumnName;
            object value = row[col];

            // Try to locate a TextBoxField first (most common for string/numeric data)
            var textField = pdfDoc.Form?.Fields?.FirstOrDefault(f => f.FullName == fieldName) as TextBoxField;
            if (textField != null)
            {
                textField.Value = value?.ToString() ?? string.Empty;
                continue;
            }

            // If not a TextBoxField, fall back to a generic field (e.g., checkbox, radio button)
            var genericField = pdfDoc.Form?.Fields?.FirstOrDefault(f => f.FullName == fieldName);
            if (genericField != null)
            {
                // Attempt to set a "Checked" property for check‑boxes/radio‑buttons via reflection
                var checkedProp = genericField.GetType().GetProperty("Checked");
                if (checkedProp != null && checkedProp.CanWrite)
                {
                    // Treat any non‑zero / non‑false value as checked
                    bool isChecked = Convert.ToBoolean(value);
                    checkedProp.SetValue(genericField, isChecked);
                }
                else
                {
                    // Fallback – set the generic Value property (works for many field types)
                    genericField.Value = value?.ToString() ?? string.Empty;
                }
            }
        }
    }

    /// <summary>
    /// Generates a list of sample SheetData objects. Each DataTable contains a few rows
    /// and columns that correspond to the field names expected in the PDF form.
    /// This replaces the original Aspose.Cells Excel‑reading logic.
    /// </summary>
    private static List<SheetData> GetSampleSheets()
    {
        var sheets = new List<SheetData>();

        // ----- Sheet 1 -----
        var dt1 = new DataTable();
        dt1.Columns.Add("Name", typeof(string));
        dt1.Columns.Add("Date", typeof(string));
        dt1.Rows.Add("John Doe", DateTime.Today.ToShortDateString());
        dt1.Rows.Add("Jane Smith", DateTime.Today.AddDays(1).ToShortDateString());
        sheets.Add(new SheetData { SheetName = "Sheet1", Table = dt1 });

        // ----- Sheet 2 -----
        var dt2 = new DataTable();
        dt2.Columns.Add("Product", typeof(string));
        dt2.Columns.Add("Quantity", typeof(int));
        dt2.Columns.Add("Price", typeof(decimal));
        dt2.Rows.Add("Widget A", 10, 2.99m);
        dt2.Rows.Add("Widget B", 5, 5.49m);
        sheets.Add(new SheetData { SheetName = "Sheet2", Table = dt2 });

        // ----- Sheet 3 (demonstrates a different set of fields) -----
        var dt3 = new DataTable();
        dt3.Columns.Add("EmployeeId", typeof(string));
        dt3.Columns.Add("HoursWorked", typeof(double));
        dt3.Rows.Add("E001", 40.0);
        dt3.Rows.Add("E002", 35.5);
        sheets.Add(new SheetData { SheetName = "Sheet3", Table = dt3 });

        return sheets;
    }
}
