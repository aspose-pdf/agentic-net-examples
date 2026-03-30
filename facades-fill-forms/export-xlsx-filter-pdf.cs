using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "subset.pdf";

        // ---------------------------------------------------------------------
        // Ensure a simple PDF template with form fields exists.
        // This prevents the runtime FileNotFoundException that occurred when the
        // original code tried to bind a non‑existent file.
        // ---------------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            // Create a new PDF document.
            Document doc = new Document();
            Page page = doc.Pages.Add();

            // Add a text box for each column we will import.
            // The field names must match the DataTable column names.
            AddTextBoxField(doc, page, "Name", 100, 700, 300, 720);
            AddTextBoxField(doc, page, "Age", 100, 660, 300, 680);
            AddTextBoxField(doc, page, "Country", 100, 620, 300, 640);

            // Save the template.
            doc.Save(templatePath);
        }

        // ---------------------------------------------------------------------
        // Simulate export from an XLSX worksheet by creating a DataTable.
        // ---------------------------------------------------------------------
        DataTable sourceTable = new DataTable();
        sourceTable.Columns.Add("Name", typeof(string));
        sourceTable.Columns.Add("Age", typeof(int));
        sourceTable.Columns.Add("Country", typeof(string));

        DataRow row1 = sourceTable.NewRow();
        row1["Name"] = "Alice";
        row1["Age"] = 28;
        row1["Country"] = "USA";
        sourceTable.Rows.Add(row1);

        DataRow row2 = sourceTable.NewRow();
        row2["Name"] = "Bob";
        row2["Age"] = 35;
        row2["Country"] = "Canada";
        sourceTable.Rows.Add(row2);

        DataRow row3 = sourceTable.NewRow();
        row3["Name"] = "Charlie";
        row3["Age"] = 42;
        row3["Country"] = "UK";
        sourceTable.Rows.Add(row3);

        // ---------------------------------------------------------------------
        // Filter rows where Age > 30.
        // ---------------------------------------------------------------------
        DataTable filteredTable = sourceTable.Clone(); // copies schema only
        foreach (DataRow sourceRow in sourceTable.Rows)
        {
            int age = (int)sourceRow["Age"];
            if (age > 30)
            {
                filteredTable.ImportRow(sourceRow);
            }
        }

        // ---------------------------------------------------------------------
        // Import the filtered data into the PDF form using AutoFiller.
        // ---------------------------------------------------------------------
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.ImportDataTable(filteredTable);
        autoFiller.Save(outputPath);

        Console.WriteLine("Subset PDF generated: " + outputPath);
    }

    /// <summary>
    /// Helper that adds a TextBox form field to a page.
    /// </summary>
    private static void AddTextBoxField(Document doc, Page page, string fieldName, float llx, float lly, float urx, float ury)
    {
        // Create a rectangle that defines the field position.
        var rect = new Rectangle(llx, lly, urx, ury);
        // Create the field and set a default value (optional).
        var txtField = new TextBoxField(page, rect)
        {
            PartialName = fieldName,
            Value = string.Empty
        };
        // Use the page's 1‑based Number property to avoid IndexOutOfRangeException.
        int pageNumber = page.Number; // page.Number is 1‑based and always valid here.
        doc.Form.Add(txtField, pageNumber);
    }
}
