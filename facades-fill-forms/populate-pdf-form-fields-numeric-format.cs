using System;
using System.Data;
using System.Globalization;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Create a sample DataTable containing numeric values
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("CustomerName", typeof(string));
        dataTable.Columns.Add("InvoiceNumber", typeof(int));
        dataTable.Columns.Add("TotalAmount", typeof(decimal));
        dataTable.Columns.Add("DueDate", typeof(DateTime));

        DataRow row1 = dataTable.NewRow();
        row1["CustomerName"] = "Acme Corp";
        row1["InvoiceNumber"] = 1023;
        row1["TotalAmount"] = 1234.567m;
        row1["DueDate"] = new DateTime(2023, 12, 31);
        dataTable.Rows.Add(row1);

        DataRow row2 = dataTable.NewRow();
        row2["CustomerName"] = "Beta Ltd";
        row2["InvoiceNumber"] = 1024;
        row2["TotalAmount"] = 9876.5m;
        row2["DueDate"] = new DateTime(2024, 1, 15);
        dataTable.Rows.Add(row2);

        // Format numeric columns as strings with two decimal places
        foreach (DataColumn column in dataTable.Columns)
        {
            Type columnType = column.DataType;
            if (columnType == typeof(int) || columnType == typeof(long) ||
                columnType == typeof(float) || columnType == typeof(double) ||
                columnType == typeof(decimal))
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    object value = dataRow[column];
                    if (value != DBNull.Value)
                    {
                        IFormattable formattable = (IFormattable)value;
                        string formatted = formattable.ToString("N2", CultureInfo.InvariantCulture);
                        dataRow[column] = formatted;
                    }
                }
            }
        }

        // Populate PDF form fields using AutoFiller
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"PDF form fields populated and saved to '{outputPath}'.");
    }
}
