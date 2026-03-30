using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // Added for FontRepository and Color

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "filled.pdf";

        // ------------------------------------------------------------
        // 1. Create a simple PDF template with AcroForm fields that match
        //    the column names of the DataTable. This guarantees that the
        //    file exists at runtime and eliminates the FileNotFoundException.
        // ------------------------------------------------------------
        CreatePdfTemplate(templatePath);

        // ------------------------------------------------------------
        // 2. Build and populate the DataTable whose column names match the
        //    form field names.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));
        dataTable.Columns.Add("Email", typeof(string));

        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        row["Email"] = "john.doe@example.com";
        dataTable.Rows.Add(row);

        // ------------------------------------------------------------
        // 3. Use AutoFiller to bind the template, import the DataTable and
        //    save the filled PDF.
        // ------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Generates a minimal PDF containing three TextBox fields named
    /// FirstName, LastName and Email. The method is called at runtime so
    /// the template file always exists, removing the original FileNotFoundException.
    /// </summary>
    private static void CreatePdfTemplate(string path)
    {
        // Create a new PDF document.
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define a helper to add a TextBox field.
        void AddTextBox(string name, float llx, float lly, float urx, float ury)
        {
            TextBoxField field = new TextBoxField(page, new Rectangle(llx, lly, urx, ury))
            {
                PartialName = name
                // DefaultAppearance is optional; omitted to avoid missing type errors.
            };
            doc.Form.Add(field);
        }

        // Add fields that correspond to the DataTable columns.
        AddTextBox("FirstName", 100, 700, 300, 720);
        AddTextBox("LastName", 100, 660, 300, 680);
        AddTextBox("Email", 100, 620, 300, 640);

        // Save the template.
        doc.Save(path);
    }
}
