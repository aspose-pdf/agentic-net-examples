using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // Added for TextBoxField

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare a DataTable whose column names match the AcroForm
        //    field names.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));
        dataTable.Columns.Add("Email", typeof(string));

        DataRow row1 = dataTable.NewRow();
        row1["FirstName"] = "John";
        row1["LastName"] = "Doe";
        row1["Email"] = "john.doe@example.com";
        dataTable.Rows.Add(row1);

        DataRow row2 = dataTable.NewRow();
        row2["FirstName"] = "Jane";
        row2["LastName"] = "Smith";
        row2["Email"] = "jane.smith@example.com";
        dataTable.Rows.Add(row2);

        // ------------------------------------------------------------
        // 2. Ensure the template PDF exists. If it does not, create a
        //    minimal PDF with AcroForm fields that match the DataTable
        //    column names. This prevents the FileNotFoundException at
        //    runtime.
        // ------------------------------------------------------------
        const string templatePath = "template.pdf";
        if (!File.Exists(templatePath))
        {
            // Create a new PDF document.
            Document templateDoc = new Document();
            Page page = templateDoc.Pages.Add();

            // Helper to add a TextBoxField at a given position.
            void AddTextBox(string fieldName, float llx, float lly, float urx, float ury)
            {
                var rect = new Rectangle(llx, lly, urx, ury);
                var txtField = new TextBoxField(page, rect)
                {
                    PartialName = fieldName,
                    Value = string.Empty
                };
                templateDoc.Form.Add(txtField);
            }

            // Add fields that correspond to the DataTable columns.
            AddTextBox("FirstName", 100, 700, 300, 720);
            AddTextBox("LastName", 100, 660, 300, 680);
            AddTextBox("Email", 100, 620, 300, 640);

            // Save the generated template.
            templateDoc.Save(templatePath);
        }

        // ------------------------------------------------------------
        // 3. Create AutoFiller, bind the (now guaranteed) template PDF,
        //    import the DataTable and save the filled result.
        // ------------------------------------------------------------
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.ImportDataTable(dataTable);
        autoFiller.Save("filled.pdf");
    }
}
