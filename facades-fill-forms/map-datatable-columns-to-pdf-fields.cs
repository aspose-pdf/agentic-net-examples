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
        string templatePath = "template.pdf";
        string outputPath = "filled.pdf";

        // Ensure a template PDF with the expected form fields exists.
        CreateTemplateIfMissing(templatePath);

        // Create a DataTable whose column names do NOT match the PDF field names.
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("AddressLine", typeof(string));
        dataTable.Columns.Add("Zip", typeof(string));

        // Populate the table with sample data.
        DataRow row = dataTable.NewRow();
        row["Name"] = "John Doe";
        row["AddressLine"] = "123 Main St";
        row["Zip"] = "12345";
        dataTable.Rows.Add(row);

        // Map DataTable column names to the exact PDF field identifiers.
        // Assume the PDF has fields: FullName, StreetAddress, PostalCode.
        RenameColumn(dataTable, "Name", "FullName");
        RenameColumn(dataTable, "AddressLine", "StreetAddress");
        RenameColumn(dataTable, "Zip", "PostalCode");

        // Use AutoFiller to merge the data into the PDF template.
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPath);
        }

        Console.WriteLine("PDF generated: " + outputPath);
    }

    private static void RenameColumn(DataTable table, string oldName, string newName)
    {
        if (table.Columns.Contains(oldName))
        {
            DataColumn column = table.Columns[oldName];
            column.ColumnName = newName;
        }
    }

    /// <summary>
    /// Creates a minimal PDF template containing the required form fields if the file does not already exist.
    /// </summary>
    private static void CreateTemplateIfMissing(string path)
    {
        if (File.Exists(path))
            return;

        // Create a new PDF document.
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Helper to add a text box form field.
        void AddTextBox(string fieldName, float llx, float lly, float urx, float ury)
        {
            // TextBoxField is a widget annotation; it must be added to the document's Form collection,
            // not directly to page.Annotations.
            TextBoxField txt = new TextBoxField(page, new Rectangle(llx, lly, urx, ury))
            {
                PartialName = fieldName,
                Value = string.Empty
            };
            doc.Form.Add(txt);
        }

        // Add the three fields expected by the AutoFiller.
        AddTextBox("FullName", 100, 700, 400, 720);
        AddTextBox("StreetAddress", 100, 660, 400, 680);
        AddTextBox("PostalCode", 100, 620, 200, 640);

        // Save the template.
        doc.Save(path);
    }
}
