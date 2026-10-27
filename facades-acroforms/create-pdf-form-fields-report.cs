using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath = "form.pdf";          // PDF containing form fields
        const string reportPdfPath = "form_report.pdf";   // Output report

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF and get its form fields via Document.Form.Fields
        // -----------------------------------------------------------------
        Document srcDoc = new Document(sourcePdfPath);
        var fields = srcDoc.Form.Fields; // Collection of Field objects

        // -----------------------------------------------------------------
        // 2. Create a new PDF document for the report
        // -----------------------------------------------------------------
        using (Document reportDoc = new Document())
        {
            // Add a single page
            Page page = reportDoc.Pages.Add();

            // -----------------------------------------------------------------
            // 3. Build a table: Header row + one row per form field
            // -----------------------------------------------------------------
            Table table = new Table
            {
                ColumnWidths = "150 100 200",
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add(CreateHeaderCell("Field Name"));
            header.Cells.Add(CreateHeaderCell("Field Type"));
            header.Cells.Add(CreateHeaderCell("Field Value"));

            // Data rows
            foreach (Field field in fields)
            {
                Row row = table.Rows.Add();

                // Field name
                row.Cells.Add(CreateDataCell(field.PartialName));

                // Field type – use runtime type name because Field does not expose FieldType
                string typeStr = field.GetType().Name;
                row.Cells.Add(CreateDataCell(typeStr));

                // Field value – use reflection/dynamic to obtain a value if the field supports it
                string value = GetFieldValueAsString(field);
                row.Cells.Add(CreateDataCell(value));
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // -----------------------------------------------------------------
            // 4. Save the report PDF
            // -----------------------------------------------------------------
            reportDoc.Save(reportPdfPath);
        }

        Console.WriteLine($"Form fields report saved to '{reportPdfPath}'.");
    }

    // Helper to create a header cell with bold text
    private static Cell CreateHeaderCell(string text)
    {
        Cell cell = new Cell
        {
            BackgroundColor = Color.LightGray,
            Border = new BorderInfo(BorderSide.All, 0.5f)
        };
        TextFragment tf = new TextFragment(text)
        {
            TextState = { FontSize = 12, FontStyle = FontStyles.Bold }
        };
        cell.Paragraphs.Add(tf);
        return cell;
    }

    // Helper to create a regular data cell
    private static Cell CreateDataCell(string text)
    {
        Cell cell = new Cell
        {
            Border = new BorderInfo(BorderSide.All, 0.5f)
        };
        TextFragment tf = new TextFragment(text)
        {
            TextState = { FontSize = 10 }
        };
        cell.Paragraphs.Add(tf);
        return cell;
    }

    // Retrieves the value of a form field as a string, handling different field types safely.
    private static string GetFieldValueAsString(Field field)
    {
        // Most concrete field types expose a "Value" property (e.g., TextBoxField, ComboBoxField).
        // Use dynamic dispatch; if the property does not exist, fall back to an empty string.
        try
        {
            dynamic dynField = field;
            var val = dynField.Value;
            return val?.ToString() ?? string.Empty;
        }
        catch
        {
            // For fields without a Value property (e.g., CheckBoxField, RadioButtonOptionField),
            // attempt to read a boolean "Checked" property.
            try
            {
                dynamic dynField = field;
                var checkedProp = dynField.Checked;
                if (checkedProp is bool b)
                    return b.ToString();
                return checkedProp?.ToString() ?? string.Empty;
            }
            catch
            {
                // Unknown field type – return empty string.
                return string.Empty;
            }
        }
    }
}
