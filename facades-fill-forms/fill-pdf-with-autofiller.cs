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
        const string templatePath = "template.pdf";
        const string outputPath = "filled.pdf";

        // Ensure a template PDF with the required form fields exists
        EnsureTemplate(templatePath);

        // Prepare sample data matching the PDF form field names
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));

        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        dataTable.Rows.Add(row);

        // Use AutoFiller to merge data into the PDF and ensure resources are released
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPath);
        }

        Console.WriteLine("Filled PDF saved to " + outputPath);
    }

    /// <summary>
    /// Creates a minimal PDF containing the form fields required for the demo
    /// if the file does not already exist.
    /// </summary>
    private static void EnsureTemplate(string path)
    {
        if (File.Exists(path))
            return;

        // Create a new PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // FirstName field
        TextBoxField firstNameField = new TextBoxField(page, new Rectangle(100, 700, 300, 720))
        {
            PartialName = "FirstName",
            Value = string.Empty
        };
        doc.Form.Add(firstNameField);

        // LastName field
        TextBoxField lastNameField = new TextBoxField(page, new Rectangle(100, 650, 300, 670))
        {
            PartialName = "LastName",
            Value = string.Empty
        };
        doc.Form.Add(lastNameField);

        // Save the template PDF
        doc.Save(path);
    }
}
