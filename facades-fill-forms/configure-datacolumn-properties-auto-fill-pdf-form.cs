using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a PDF template with form fields "Name" and "ID".
        // ------------------------------------------------------------
        const string templatePath = "template.pdf";
        using (Document templateDoc = new Document())
        {
            // Add a single blank page.
            templateDoc.Pages.Add();

            // Create a text box for the "Name" field.
            TextBoxField nameField = new TextBoxField(
                templateDoc.Pages[1],
                new Rectangle(100, 700, 300, 730) // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            );
            nameField.PartialName = "Name"; // field name must match DataTable column name
            templateDoc.Form.Add(nameField, 1);

            // Create a text box for the "ID" field.
            TextBoxField idField = new TextBoxField(
                templateDoc.Pages[1],
                new Rectangle(100, 650, 300, 680)
            );
            idField.PartialName = "ID";
            templateDoc.Form.Add(idField, 1);

            // Save the template so AutoFiller can bind to it.
            templateDoc.Save(templatePath);
        }

        // ------------------------------------------------------------
        // 2. Create and configure the DataTable.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");

        // Column that can be edited and does not need to be unique.
        DataColumn nameColumn = dataTable.Columns.Add("Name", typeof(string));
        nameColumn.ReadOnly = false;   // allow editing in the PDF form
        nameColumn.Unique   = false;   // duplicate values are allowed

        // Column that is read‑only and must be unique.
        DataColumn idColumn = dataTable.Columns.Add("ID", typeof(int));
        idColumn.ReadOnly = true;      // field will be locked in the PDF form
        idColumn.Unique   = true;      // each value must be unique

        // Add sample rows (ID values must be unique).
        dataTable.Rows.Add("Alice", 1);
        dataTable.Rows.Add("Bob",   2);
        dataTable.Rows.Add("Carol", 3);

        // ------------------------------------------------------------
        // 3. Import the DataTable into the PDF form using AutoFiller.
        // ------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF template that we just created.
            autoFiller.BindPdf(templatePath);

            // Import the configured DataTable.
            autoFiller.ImportDataTable(dataTable);

            // Save the filled PDF.
            autoFiller.Save("filled_output.pdf");
        }
    }
}
