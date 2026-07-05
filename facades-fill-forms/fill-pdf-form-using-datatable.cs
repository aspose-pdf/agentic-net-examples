using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the template PDF and the output PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // ------------------------------------------------------------
        // 0. Ensure a PDF template with the required form fields exists.
        // ------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            CreateTemplatePdf(templatePath);
        }

        // ------------------------------------------------------------
        // 1. Create a DataTable that will hold the data for the form.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");

        // Use the DataColumnCollection to add columns.
        // Each column name must match a field name in the PDF form.
        DataColumnCollection columns = dataTable.Columns;
        columns.Add("FirstName", typeof(string));
        columns.Add("LastName",  typeof(string));
        columns.Add("Email",     typeof(string));
        columns.Add("Age",       typeof(int));
        // Example of adding a custom column that does NOT exist in the form.
        columns.Add("CustomNote", typeof(string));

        // ------------------------------------------------------------
        // 2. Populate the DataTable with a single row of data.
        // ------------------------------------------------------------
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Email"]     = "john.doe@example.com";
        row["Age"]       = 30;
        row["CustomNote"] = "Sample note";
        dataTable.Rows.Add(row);

        // ------------------------------------------------------------
        // 3. Use Aspose.Pdf.Facades.AutoFiller to import the data.
        // ------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF template that contains the form fields.
            autoFiller.BindPdf(templatePath);

            // Import the DataTable. Column names must match field names exactly.
            autoFiller.ImportDataTable(dataTable);

            // Save the filled PDF.
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Creates a simple PDF file containing form fields that match the column names used in the DataTable.
    /// </summary>
    /// <param name="path">File path where the template PDF will be saved.</param>
    private static void CreateTemplatePdf(string path)
    {
        // Create a new PDF document.
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Helper to add a TextBoxField.
        void AddTextBox(string name, double llx, double lly, double urx, double ury)
        {
            // TextBoxField constructor expects a Page and a Rectangle.
            var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            TextBoxField field = new TextBoxField(page, rect);
            field.PartialName = name;
            field.Value = string.Empty; // default empty value
            doc.Form.Add(field);
        }

        // Add fields that correspond to the DataTable columns.
        AddTextBox("FirstName", 100, 700, 300, 720);
        AddTextBox("LastName",  100, 660, 300, 680);
        AddTextBox("Email",     100, 620, 300, 640);
        AddTextBox("Age",       100, 580, 300, 600);

        // Save the template PDF.
        doc.Save(path);
    }
}
