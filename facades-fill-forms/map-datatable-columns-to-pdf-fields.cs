using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "filled.pdf";

        // Ensure a template PDF with the required form fields exists.
        if (!File.Exists(templatePath))
        {
            CreateTemplatePdf(templatePath);
        }

        // Prepare a sample DataTable with source column names.
        DataTable dataTable = new DataTable("Data");
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Address", typeof(string));
        dataTable.Rows.Add("John Doe", "123 Main St");
        dataTable.Rows.Add("Jane Smith", "456 Oak Ave");

        // Mapping from DataTable column names to PDF form field identifiers.
        Dictionary<string, string> columnToFieldMap = new Dictionary<string, string>
        {
            { "Name", "FullName" },
            { "Address", "Location" }
        };

        // Rename DataTable columns so they exactly match the PDF field names.
        foreach (KeyValuePair<string, string> kvp in columnToFieldMap)
        {
            if (dataTable.Columns.Contains(kvp.Key))
            {
                dataTable.Columns[kvp.Key].ColumnName = kvp.Value;
            }
        }

        // Use AutoFiller to bind the template PDF and import the adjusted DataTable.
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePath);
            filler.ImportDataTable(dataTable);
            filler.Save(outputPath);
        }

        Console.WriteLine("PDF generated: " + outputPath);
    }

    private static void CreateTemplatePdf(string path)
    {
        // Create a simple PDF with two text box form fields: FullName and Location.
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // FullName field
        TextBoxField fullName = new TextBoxField(page, new Rectangle(100, 700, 300, 720));
        fullName.PartialName = "FullName";
        fullName.Value = ""; // default empty value
        doc.Form.Add(fullName);

        // Location field
        TextBoxField location = new TextBoxField(page, new Rectangle(100, 650, 300, 670));
        location.PartialName = "Location";
        location.Value = "";
        doc.Form.Add(location);

        doc.Save(path);
    }
}
