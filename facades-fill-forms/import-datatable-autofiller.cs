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
        // Prepare a DataTable whose column names match the PDF form field names.
        DataTable dataTable = new DataTable("MailMerge");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));
        dataTable.Columns.Add("Address", typeof(string));

        DataRow row1 = dataTable.NewRow();
        row1["FirstName"] = "John";
        row1["LastName"] = "Doe";
        row1["Address"] = "123 Main St";
        dataTable.Rows.Add(row1);

        DataRow row2 = dataTable.NewRow();
        row2["FirstName"] = "Jane";
        row2["LastName"] = "Smith";
        row2["Address"] = "456 Oak Ave";
        dataTable.Rows.Add(row2);

        // Create a simple PDF template in memory that contains AcroForm fields
        // matching the column names.
        using (MemoryStream templateStream = CreatePdfTemplate())
        {
            // Bind the template using the stream overload (avoids file‑system dependency).
            AutoFiller autoFiller = new AutoFiller();
            autoFiller.BindPdf(templateStream);

            // Import the DataTable – each column name must match a field name (case‑sensitive).
            autoFiller.ImportDataTable(dataTable);

            // Save the merged result to a new PDF file.
            string outputPath = "filled.pdf";
            autoFiller.Save(outputPath);
        }
    }

    // Generates a one‑page PDF containing TextBox fields named FirstName, LastName and Address.
    private static MemoryStream CreatePdfTemplate()
    {
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Helper to add a TextBox field to the page.
        void AddTextBox(string name, float llx, float lly, float urx, float ury)
        {
            TextBoxField field = new TextBoxField(page, new Rectangle(llx, lly, urx, ury));
            field.PartialName = name;
            field.Value = string.Empty; // initial empty value
            doc.Form.Add(field);
        }

        // Position the fields (adjust coordinates as needed for your layout).
        AddTextBox("FirstName", 100, 700, 300, 720);
        AddTextBox("LastName", 100, 650, 300, 670);
        AddTextBox("Address", 100, 600, 400, 620);

        MemoryStream ms = new MemoryStream();
        doc.Save(ms);
        ms.Position = 0; // rewind for reading
        return ms;
    }
}