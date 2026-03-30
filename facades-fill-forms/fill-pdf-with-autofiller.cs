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

        // Ensure the template PDF exists. If it does not, create a minimal PDF with the required form fields.
        if (!File.Exists(templatePath))
        {
            // Create a new PDF document.
            var doc = new Document();
            var page = doc.Pages.Add();

            // Add a text box for the "Name" field.
            var nameField = new TextBoxField(page, new Rectangle(100, 700, 300, 720))
            {
                PartialName = "Name"
            };
            doc.Form.Add(nameField);

            // Add a text box for the "Address" field.
            var addressField = new TextBoxField(page, new Rectangle(100, 650, 300, 670))
            {
                PartialName = "Address"
            };
            doc.Form.Add(addressField);

            // Save the template PDF.
            doc.Save(templatePath);
        }

        // Prepare sample data matching the field names in the PDF template.
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Address", typeof(string));
        DataRow dataRow = dataTable.NewRow();
        dataRow["Name"] = "John Doe";
        dataRow["Address"] = "123 Main St";
        dataTable.Rows.Add(dataRow);

        // AutoFiller fills the template and saves the result.
        // The using statement guarantees that AutoFiller is disposed after saving,
        // releasing any unmanaged resources it holds.
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPath);
        }

        Console.WriteLine("Filled PDF saved to " + outputPath);
    }
}
