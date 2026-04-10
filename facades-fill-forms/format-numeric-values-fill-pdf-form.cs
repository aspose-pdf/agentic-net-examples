using System;
using System.Data;
using System.IO;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Build a DataTable whose column names match the PDF form field names
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("CustomerName", typeof(string));
        dataTable.Columns.Add("TotalAmount",  typeof(decimal));
        dataTable.Columns.Add("InvoiceDate", typeof(DateTime));

        // Sample rows
        dataTable.Rows.Add("Alice", 1234.567m, DateTime.Today);
        dataTable.Rows.Add("Bob",   89.1m,    DateTime.Today.AddDays(1));

        // ---- Numeric formatting -------------------------------------------------
        // Convert numeric values to the desired string representation (e.g., two decimals)
        foreach (DataRow row in dataTable.Rows)
        {
            decimal amount = (decimal)row["TotalAmount"];
            row["TotalAmount"] = amount.ToString("F2"); // "1234.57"
        }

        // AutoFiller expects text values; ensure every column is of type string
        DataTable formattedTable = new DataTable("FormDataFormatted");
        foreach (DataColumn col in dataTable.Columns)
        {
            formattedTable.Columns.Add(col.ColumnName, typeof(string));
        }

        foreach (DataRow row in dataTable.Rows)
        {
            DataRow newRow = formattedTable.NewRow();
            foreach (DataColumn col in dataTable.Columns)
            {
                newRow[col.ColumnName] = row[col.ColumnName].ToString();
            }
            formattedTable.Rows.Add(newRow);
        }

        // ---- Fill PDF fields ----------------------------------------------------
        using (Aspose.Pdf.Facades.AutoFiller filler = new Aspose.Pdf.Facades.AutoFiller())
        {
            filler.BindPdf(templatePath);                 // Load the template PDF
            filler.ImportDataTable(formattedTable);       // Populate fields
            filler.Save(outputPath);                      // Write the result
        }

        Console.WriteLine($"PDF with formatted numbers saved to '{outputPath}'.");
    }
}