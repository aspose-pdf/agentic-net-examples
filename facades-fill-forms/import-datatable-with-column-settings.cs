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

        // Ensure a template PDF exists – create a minimal one if it does not.
        if (!File.Exists(templatePath))
        {
            CreateSimpleTemplate(templatePath);
        }

        // Create a DataTable and configure column properties
        DataTable dataTable = new DataTable();

        DataColumn firstNameColumn = new DataColumn("FirstName", typeof(string))
        {
            ReadOnly = false,
            Unique = false
        };
        dataTable.Columns.Add(firstNameColumn);

        DataColumn ageColumn = new DataColumn("Age", typeof(int))
        {
            ReadOnly = true,   // make this column read‑only
            Unique = true     // enforce unique values
        };
        dataTable.Columns.Add(ageColumn);

        // Add a sample row
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["Age"] = 30;
        dataTable.Rows.Add(row);

        // Import the DataTable into the PDF form using AutoFiller
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Creates a very simple PDF containing two text box fields (FirstName and Age).
    /// This method is only invoked when the expected template file is missing, preventing
    /// a runtime FileNotFoundException.
    /// </summary>
    private static void CreateSimpleTemplate(string path)
    {
        // Create a new PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define rectangle positions for the fields (left, bottom, right, top)
        var firstNameRect = new Rectangle(100, 600, 300, 620);
        var ageRect = new Rectangle(100, 560, 300, 580);

        // FirstName text box field
        TextBoxField firstNameField = new TextBoxField(page, firstNameRect)
        {
            PartialName = "FirstName",
            Value = string.Empty
        };
        doc.Form.Add(firstNameField);

        // Age text box field
        TextBoxField ageField = new TextBoxField(page, ageRect)
        {
            PartialName = "Age",
            Value = string.Empty
        };
        doc.Form.Add(ageField);

        // Save the template for later use
        doc.Save(path);
    }
}
