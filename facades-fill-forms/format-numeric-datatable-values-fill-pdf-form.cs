using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the template PDF and the resulting filled PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // ------------------------------------------------------------
        // Create a PDF template with form fields that match the DataTable column names.
        // ------------------------------------------------------------
        if (!System.IO.File.Exists(templatePath))
        {
            Document templateDoc = new Document();
            Page page = templateDoc.Pages.Add();

            // Define positions for the three fields (you can adjust as needed).
            float left = 100f, width = 300f, height = 20f;
            float startY = 750f, verticalSpacing = 30f;

            // CustomerName field
            Rectangle rectName = new Rectangle(left, startY, left + width, startY + height);
            TextBoxField nameField = new TextBoxField(page, rectName) { PartialName = "CustomerName" };
            templateDoc.Form.Add(nameField);

            // InvoiceAmount field
            Rectangle rectAmount = new Rectangle(left, startY - verticalSpacing, left + width, startY - verticalSpacing + height);
            TextBoxField amountField = new TextBoxField(page, rectAmount) { PartialName = "InvoiceAmount" };
            templateDoc.Form.Add(amountField);

            // InvoiceDate field
            Rectangle rectDate = new Rectangle(left, startY - 2 * verticalSpacing, left + width, startY - 2 * verticalSpacing + height);
            TextBoxField dateField = new TextBoxField(page, rectDate) { PartialName = "InvoiceDate" };
            templateDoc.Form.Add(dateField);

            templateDoc.Save(templatePath);
        }

        // ------------------------------------------------------------
        // Create and populate a DataTable with sample data.
        // ------------------------------------------------------------
        DataTable data = new DataTable("FormData");
        data.Columns.Add("CustomerName", typeof(string));
        data.Columns.Add("InvoiceAmount", typeof(decimal));
        data.Columns.Add("InvoiceDate", typeof(DateTime));

        data.Rows.Add("Alice", 1234.567m, DateTime.Today);
        data.Rows.Add("Bob",   89.10m,   DateTime.Today);

        // ------------------------------------------------------------
        // Build a new DataTable where numeric values are formatted as strings ("N2").
        // ------------------------------------------------------------
        DataTable formatted = new DataTable();
        foreach (DataColumn col in data.Columns)
        {
            if (col.ColumnName == "InvoiceAmount")
                formatted.Columns.Add(col.ColumnName, typeof(string)); // formatted as string
            else
                formatted.Columns.Add(col.ColumnName, col.DataType);
        }

        foreach (DataRow row in data.Rows)
        {
            DataRow newRow = formatted.NewRow();
            foreach (DataColumn col in data.Columns)
            {
                if (col.ColumnName == "InvoiceAmount" && row[col] != DBNull.Value)
                {
                    decimal amount = Convert.ToDecimal(row[col]);
                    newRow[col.ColumnName] = amount.ToString("N2"); // two decimal places with thousand separator
                }
                else
                {
                    newRow[col.ColumnName] = row[col];
                }
            }
            formatted.Rows.Add(newRow);
        }

        // ------------------------------------------------------------
        // Use AutoFiller to merge the formatted DataTable into the PDF.
        // ------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);
            autoFiller.ImportDataTable(formatted);
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}
