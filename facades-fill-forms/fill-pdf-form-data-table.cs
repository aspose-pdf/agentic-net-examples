using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for Border and BorderStyle

class Program
{
    static void Main()
    {
        // Paths to the template PDF and the output PDF
        const string templatePath = "template.pdf";
        const string outputPath = "filled.pdf";

        // ------------------------------------------------------------
        // 1. Build a DataTable whose column names match the form field names.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");
        DataColumnCollection columns = dataTable.Columns;
        columns.Add("FirstName", typeof(string));
        columns.Add("LastName", typeof(string));
        columns.Add("Address", typeof(string));
        columns.Add("City", typeof(string));
        columns.Add("PostalCode", typeof(string));
        columns.Add("Country", typeof(string));
        // Custom column that can be used for a calculated field in the PDF form
        columns.Add("Greeting", typeof(string));

        // Populate a single row with sample data
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        row["Address"] = "123 Main St.";
        row["City"] = "Anytown";
        row["PostalCode"] = "12345";
        row["Country"] = "USA";
        row["Greeting"] = "Dear John,";
        dataTable.Rows.Add(row);

        // ------------------------------------------------------------
        // 2. Ensure the template PDF exists. If it does not, create a minimal
        //    PDF with form fields that match the DataTable column names.
        // ------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            // Create a new PDF document
            Document doc = new Document();
            Page page = doc.Pages.Add();

            // Simple layout: place each field one under another.
            float yPos = 750; // start near the top of the page
            const float left = 100;
            const float width = 300;
            const float height = 20;
            const float verticalSpacing = 30;

            foreach (DataColumn col in columns)
            {
                // Define a rectangle for the field
                Rectangle rect = new Rectangle(left, yPos, left + width, yPos + height);
                // Create a textbox field and assign the column name as its PartialName
                TextBoxField txtField = new TextBoxField(page, rect)
                {
                    PartialName = col.ColumnName
                };
                // Set the border (no Color property on Border)
                txtField.Border = new Border(txtField)
                {
                    Style = BorderStyle.Solid,
                    Width = 1
                };
                // Set the border color via the field's own Color property
                txtField.Color = Color.Black;

                doc.Form.Add(txtField);
                yPos -= verticalSpacing;
            }

            // Save the generated template for later use
            doc.Save(templatePath);
        }

        // ------------------------------------------------------------
        // 3. Use AutoFiller to bind the template and import the DataTable.
        // ------------------------------------------------------------
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.ImportDataTable(dataTable);
        autoFiller.Save(outputPath);

        Console.WriteLine($"PDF filled successfully. Output saved to '{outputPath}'.");
    }
}
