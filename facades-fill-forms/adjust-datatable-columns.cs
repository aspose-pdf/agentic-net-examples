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
        const string templatePath = "template.pdf";
        const string outputPath = "filled.pdf";

        // Ensure a template PDF with the expected form fields exists.
        // If the file is missing, create a minimal PDF with the required fields.
        if (!File.Exists(templatePath))
        {
            CreateTemplatePdf(templatePath);
        }

        // Create a DataTable whose column names do NOT initially match the PDF field names
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));
        dataTable.Columns.Add("Email", typeof(string));

        DataRow dataRow = dataTable.NewRow();
        dataRow["FirstName"] = "John";
        dataRow["LastName"] = "Doe";
        dataRow["Email"] = "john.doe@example.com";
        dataTable.Rows.Add(dataRow);

        // The PDF form fields are named "First_Name", "Last_Name", "Email_Address"
        // Rename the DataTable columns to match those identifiers
        dataTable.Columns["FirstName"].ColumnName = "First_Name";
        dataTable.Columns["LastName"].ColumnName = "Last_Name";
        dataTable.Columns["Email"].ColumnName = "Email_Address";

        // Fill the PDF form using AutoFiller
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.ImportDataTable(dataTable);
        autoFiller.Save(outputPath);

        Console.WriteLine($"Form filled and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Creates a simple PDF file containing three text box form fields that match the identifiers
    /// used in the DataTable after renaming.
    /// </summary>
    private static void CreateTemplatePdf(string path)
    {
        // Create a new PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Helper to add a textbox field at a given position
        void AddTextBox(string fieldName, float llx, float lly, float urx, float ury)
        {
            TextBoxField txt = new TextBoxField(page, new Rectangle(llx, lly, urx, ury))
            {
                PartialName = fieldName,
                Value = string.Empty
            };

            // Set a solid black border using the correct Aspose.Pdf.Annotations.Border class
            txt.Border = new Border(txt)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };
            // Border colour is defined by the annotation's own Color property
            txt.Color = Color.Black;

            doc.Form.Add(txt);
        }

        // Add fields with the exact names expected by the AutoFiller mapping
        AddTextBox("First_Name", 100, 700, 300, 720);
        AddTextBox("Last_Name", 100, 660, 300, 680);
        AddTextBox("Email_Address", 100, 620, 400, 640);

        // Save the template PDF
        doc.Save(path);
    }
}
