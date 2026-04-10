using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // ---------------------------------------------------------------------
        // Ensure that a template PDF exists. If it does not, create a minimal one
        // containing the form fields that will be filled later. This prevents the
        // runtime FileNotFoundException that was previously thrown.
        // ---------------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            CreateTemplate(templatePath);
        }

        // ---------------------------------------------------------------------
        // Prepare a DataTable whose column names match the form field names in the
        // PDF ("Name" and "Address").
        // ---------------------------------------------------------------------
        DataTable data = new DataTable();
        data.Columns.Add("Name",    typeof(string));
        data.Columns.Add("Address", typeof(string));

        DataRow row = data.NewRow();
        row["Name"]    = "John Doe";
        row["Address"] = "123 Main St";
        data.Rows.Add(row);

        // ---------------------------------------------------------------------
        // Use a using‑statement so that AutoFiller is disposed automatically
        // after the PDF is saved. This releases the unmanaged resources held by
        // the facade.
        // ---------------------------------------------------------------------
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePath);   // Bind the template PDF file
            filler.ImportDataTable(data);   // Import the data table
            filler.Save(outputPath);        // Save the filled PDF to a file
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }

    // -------------------------------------------------------------------------
    // Helper method that creates a very simple PDF containing two text box
    // fields named "Name" and "Address". This method is only executed when the
    // expected template file is missing, making the sample self‑contained.
    // -------------------------------------------------------------------------
    private static void CreateTemplate(string path)
    {
        // Create a new PDF document.
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define a rectangle for the "Name" field (left, bottom, right, top).
        Rectangle nameRect = new Rectangle(100, 700, 300, 720);
        Aspose.Pdf.Forms.TextBoxField nameField = new Aspose.Pdf.Forms.TextBoxField(page, nameRect)
        {
            PartialName = "Name",
            Value = string.Empty
        };
        doc.Form.Add(nameField);

        // Define a rectangle for the "Address" field.
        Rectangle addressRect = new Rectangle(100, 650, 300, 670);
        Aspose.Pdf.Forms.TextBoxField addressField = new Aspose.Pdf.Forms.TextBoxField(page, addressRect)
        {
            PartialName = "Address",
            Value = string.Empty
        };
        doc.Form.Add(addressField);

        // Save the template PDF.
        doc.Save(path);
    }
}