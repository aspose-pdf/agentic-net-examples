using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled_output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Prepare a sample DataTable. In real scenarios this would come from a database or other source.
        DataTable dataTable = CreateSampleDataTable();

        // Load the PDF template.
        using (Document pdfDoc = new Document(templatePath))
        {
            // Bind the document to the Form facade for field operations.
            using (Form pdfForm = new Form(pdfDoc))
            {
                // Iterate through each row of the DataTable.
                foreach (DataRow row in dataTable.Rows)
                {
                    // For each column, the column name must match a field name in the PDF.
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        string fieldName = col.ColumnName;
                        object rawValue = row[col];

                        // Convert the raw value to a string with proper numeric formatting if needed.
                        string fieldValue = FormatValue(rawValue);

                        // Fill the field. The method returns true if the field exists and is filled.
                        bool filled = pdfForm.FillField(fieldName, fieldValue);
                        if (!filled)
                        {
                            Console.WriteLine($"Warning: Field '{fieldName}' not found in the PDF.");
                        }
                    }
                }

                // Save the modified document.
                pdfDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF fields populated and saved to '{outputPath}'.");
    }

    // Creates a sample DataTable with mixed data types.
    private static DataTable CreateSampleDataTable()
    {
        DataTable table = new DataTable("FormData");
        table.Columns.Add("CustomerName", typeof(string));
        table.Columns.Add("InvoiceNumber", typeof(int));
        table.Columns.Add("TotalAmount", typeof(decimal));
        table.Columns.Add("DueDate", typeof(DateTime));

        // Add a single row of sample data.
        DataRow row = table.NewRow();
        row["CustomerName"]   = "Acme Corp.";
        row["InvoiceNumber"]  = 1024;
        row["TotalAmount"]    = 12345.678m; // Will be formatted.
        row["DueDate"]        = new DateTime(2023, 12, 31);
        table.Rows.Add(row);

        return table;
    }

    // Formats the value: numeric types get a culture‑invariant format with two decimal places.
    private static string FormatValue(object value)
    {
        if (value == null || value == DBNull.Value)
            return string.Empty;

        switch (Type.GetTypeCode(value.GetType()))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.Int16:
            case TypeCode.UInt16:
            case TypeCode.Int32:
            case TypeCode.UInt32:
            case TypeCode.Int64:
            case TypeCode.UInt64:
                // Integer values – no decimal places.
                return Convert.ToInt64(value).ToString(System.Globalization.CultureInfo.InvariantCulture);

            case TypeCode.Single:
            case TypeCode.Double:
            case TypeCode.Decimal:
                // Floating‑point values – format with two decimal places.
                return Convert.ToDecimal(value).ToString("N2", System.Globalization.CultureInfo.InvariantCulture);

            case TypeCode.DateTime:
                // Date values – use short date pattern.
                return ((DateTime)value).ToString("d", System.Globalization.CultureInfo.InvariantCulture);

            default:
                // All other types – use default ToString().
                return value.ToString();
        }
    }
}