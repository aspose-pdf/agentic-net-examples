using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a sample PDF containing a single text box form field
        using (Document templateDoc = new Document())
        {
            Page page = templateDoc.Pages.Add();
            Rectangle fieldRect = new Rectangle(100, 600, 300, 650);
            TextBoxField nameField = new TextBoxField(page, fieldRect);
            nameField.PartialName = "Name";
            templateDoc.Form.Add(nameField);
            templateDoc.Save("template.pdf");
        }

        // Prepare data that matches the form field name
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("Name", typeof(string));
        DataRow dataRow = dataTable.NewRow();
        dataRow["Name"] = "John Doe";
        dataTable.Rows.Add(dataRow);

        // Fill the PDF using AutoFiller and ensure it is disposed
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf("template.pdf");
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save("filled.pdf");
        }
    }
}