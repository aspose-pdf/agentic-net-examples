using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF with a text box form field
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define rectangle for the form field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box field named "Name"
            TextBoxField txtField = new TextBoxField(page, rect);
            txtField.PartialName = "Name";
            txtField.Value = "Default";

            // Add the field to the document's form on page 1
            doc.Form.Add(txtField, 1);

            // Save the sample PDF
            doc.Save("input.pdf");
        }

        // Step 2: Prepare a DataTable and configure column properties
        DataTable table = new DataTable("FormData");

        // Column 1: Name (editable, not unique)
        DataColumn colName = new DataColumn("Name", typeof(string));
        colName.ReadOnly = false;
        colName.Unique = false;
        table.Columns.Add(colName);

        // Column 2: ID (read‑only, unique)
        DataColumn colId = new DataColumn("ID", typeof(int));
        colId.ReadOnly = true;
        colId.Unique = true;
        table.Columns.Add(colId);

        // Add rows (evaluation mode limits to 4 rows, we use 2 rows)
        DataRow row1 = table.NewRow();
        row1["Name"] = "Alice";
        row1["ID"] = 1;
        table.Rows.Add(row1);

        DataRow row2 = table.NewRow();
        row2["Name"] = "Bob";
        row2["ID"] = 2;
        table.Rows.Add(row2);

        // Step 3: Fill the PDF form using AutoFiller
        AutoFiller autofiller = new AutoFiller();
        autofiller.BindPdf("input.pdf");
        autofiller.ImportDataTable(table);
        autofiller.Save("output.pdf");

        Console.WriteLine("Form fields populated and saved to output.pdf");
    }
}
