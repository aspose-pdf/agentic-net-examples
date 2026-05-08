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
        const string templatePdf = "TemplateForm.pdf";
        const string outputPdf   = "FilledForm.pdf";

        // -------------------------------------------------
        // 1. Ensure a PDF template exists (create a minimal one if missing)
        // -------------------------------------------------
        if (!File.Exists(templatePdf))
        {
            CreateTemplatePdf(templatePdf);
        }

        // -------------------------------------------------
        // 2. Build the DataTable and configure column flags
        // -------------------------------------------------
        DataTable table = new DataTable("FormData");

        // Columns – names must match field names in the PDF form
        DataColumn colName = table.Columns.Add("FullName", typeof(string));
        DataColumn colAge  = table.Columns.Add("Age", typeof(int));
        DataColumn colId   = table.Columns.Add("CustomerID", typeof(string));

        // Configure column properties BEFORE importing the table
        colName.ReadOnly = false;   // editable field
        colAge.ReadOnly  = false;
        colId.ReadOnly   = true;    // make ID field read‑only in the DataTable
        colId.Unique     = true;    // ensure each ID is unique

        // Sample rows
        table.Rows.Add("John Doe", 30, "CUST001");
        table.Rows.Add("Jane Smith", 25, "CUST002");
        // Uncommenting the line below would raise a Unique‑constraint exception
        // table.Rows.Add("Bob Brown", 40, "CUST001");

        // -------------------------------------------------
        // 3. Use AutoFiller (Aspose.Pdf.Facades) to import data
        // -------------------------------------------------
        AutoFiller filler = new AutoFiller();
        filler.BindPdf(templatePdf);
        filler.ImportDataTable(table);
        filler.Save(outputPdf);

        Console.WriteLine($"Form filled and saved to '{outputPdf}'.");
    }

    // -------------------------------------------------
    // Helper: creates a very simple PDF containing form fields
    // -------------------------------------------------
    private static void CreateTemplatePdf(string path)
    {
        // Create a new PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // FullName field
        TextBoxField nameField = new TextBoxField(page, new Rectangle(100, 700, 300, 720))
        {
            PartialName = "FullName"
        };
        // Add the field to the document's form collection (not to page.Annotations)
        doc.Form.Add(nameField);

        // Age field
        TextBoxField ageField = new TextBoxField(page, new Rectangle(100, 650, 150, 670))
        {
            PartialName = "Age"
        };
        doc.Form.Add(ageField);

        // CustomerID field (read‑only in the PDF as well)
        TextBoxField idField = new TextBoxField(page, new Rectangle(100, 600, 250, 620))
        {
            PartialName = "CustomerID",
            ReadOnly    = true
        };
        doc.Form.Add(idField);

        // Save the generated template
        doc.Save(path);
    }
}
