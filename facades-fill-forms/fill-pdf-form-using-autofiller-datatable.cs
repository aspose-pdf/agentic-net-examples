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
        // Paths for the template and the output PDF.
        const string templatePath = "template.pdf";
        const string outputPath   = "filled_output.pdf";

        // ------------------------------------------------------------
        // Create and populate a DataTable. Column names must match
        // the PartialName of the form fields that will be created in
        // the template PDF.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName",  typeof(string));
        dataTable.Columns.Add("Address",   typeof(string));

        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Address"]   = "123 Main St";
        dataTable.Rows.Add(row);

        // ------------------------------------------------------------
        // Ensure a PDF template with matching AcroForm fields exists.
        // If the file is missing, create it on‑the‑fly.
        // ------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            Document templateDoc = new Document();
            Page page = templateDoc.Pages.Add();

            float yPos = 750;                     // starting Y coordinate
            const float left = 100;               // left X coordinate
            const float width = 300;              // field width
            const float height = 20;              // field height
            const float verticalSpacing = 30;     // space between fields

            foreach (DataColumn col in dataTable.Columns)
            {
                Rectangle rect = new Rectangle(left, yPos, left + width, yPos + height);
                TextBoxField field = new TextBoxField(page, rect)
                {
                    PartialName = col.ColumnName,
                    Value = string.Empty
                };
                templateDoc.Form.Add(field);
                yPos -= verticalSpacing;
            }

            templateDoc.Save(templatePath);
        }

        // ------------------------------------------------------------
        // Use AutoFiller to bind the template, import the DataTable,
        // and save the filled PDF.
        // ------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}
