using System;
using System.Data;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template not found: {pdfTemplatePath}");
            return;
        }

        // Example DataTable; replace with actual data source as needed
        DataTable table = GetSampleData();

        // Load the PDF document
        Document pdfDocument = new Document(pdfTemplatePath);

        // Populate fields from the DataTable
        foreach (DataRow row in table.Rows)
        {
            foreach (DataColumn col in table.Columns)
            {
                string fieldName = col.ColumnName;
                object value = row[col];

                string formattedValue;

                // Format numeric types with two decimal places, using invariant culture
                if (value is IFormattable && !(value is string))
                {
                    formattedValue = ((IFormattable)value).ToString("N2", CultureInfo.InvariantCulture);
                }
                else if (value is DateTime dt)
                {
                    // Example date formatting – adjust pattern as required
                    formattedValue = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    formattedValue = value?.ToString() ?? string.Empty;
                }

                // Attempt to set the field; ignore if the field does not exist
                try
                {
                    if (pdfDocument.Form.HasField(fieldName))
                    {
                        // The Form indexer returns a WidgetAnnotation; cast it to Field before using Value
                        Field field = pdfDocument.Form[fieldName] as Field;
                        if (field != null)
                        {
                            field.Value = formattedValue;
                        }
                        else
                        {
                            Console.Error.WriteLine($"Field '{fieldName}' exists but is not a form field that supports a Value property.");
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine($"Field '{fieldName}' not found in the PDF template.");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to set field '{fieldName}': {ex.Message}");
                }
            }
        }

        // Save the filled PDF
        pdfDocument.Save(outputPdfPath);

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }

    // Sample method to create a DataTable; replace with real data retrieval logic
    static DataTable GetSampleData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("InvoiceNumber", typeof(string));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("Tax", typeof(double));
        dt.Columns.Add("Date", typeof(DateTime));

        dt.Rows.Add("INV-001", 1234.5m, 123.45, DateTime.Today);
        return dt;
    }
}
