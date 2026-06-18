using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with a single text box field named "Name"
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            doc.Pages.Add();
            Aspose.Pdf.Forms.TextBoxField textField = new Aspose.Pdf.Forms.TextBoxField(doc.Pages[1], new Aspose.Pdf.Rectangle(100, 700, 300, 650));
            textField.PartialName = "Name";
            doc.Form.Add(textField);
            doc.Save("template.pdf");
        }

        // Prepare a DataTable with a column that matches the field name
        System.Data.DataTable dataTable = new System.Data.DataTable("Data");
        dataTable.Columns.Add("Name", typeof(string));
        System.Data.DataRow dataRow = dataTable.NewRow();
        dataRow["Name"] = "John Doe";
        dataTable.Rows.Add(dataRow);

        // Use AutoFiller to import the DataTable and generate the filled PDF
        using (Aspose.Pdf.Facades.AutoFiller autoFiller = new Aspose.Pdf.Facades.AutoFiller())
        {
            autoFiller.InputFileName = "template.pdf";
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save("output.pdf");
        }
    }
}