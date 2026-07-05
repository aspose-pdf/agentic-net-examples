using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // needed for TextFragment and Position

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a simple PDF form template in memory (so the file always exists)
        // ---------------------------------------------------------------------
        const string templatePath = "FormTemplate.pdf";
        const string outputPath   = "FormFilled.pdf";

        CreatePdfFormTemplate(templatePath);

        // ---------------------------------------------------------------------
        // 2. Create and configure the DataTable that will supply data
        // ---------------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");

        // Column that will be mapped to a PDF field named "CustomerID"
        DataColumn colCustomerId = new DataColumn("CustomerID", typeof(string))
        {
            // Make the column read‑only – its value cannot be changed after being set
            ReadOnly = true,
            // Ensure each value is unique across all rows
            Unique   = true
        };
        dataTable.Columns.Add(colCustomerId);

        // Another column without special constraints
        DataColumn colName = new DataColumn("CustomerName", typeof(string));
        dataTable.Columns.Add(colName);

        // Populate the table with sample data
        dataTable.Rows.Add("C001", "Acme Corp.");
        dataTable.Rows.Add("C002", "Globex Ltd.");

        // ---------------------------------------------------------------------
        // 3. Use Aspose.Pdf.Facades.AutoFiller to import the table into the PDF form
        // ---------------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF form template that we just created
            autoFiller.BindPdf(templatePath);

            // Import the configured DataTable – column names must match field names
            autoFiller.ImportDataTable(dataTable);

            // Save the resulting PDF
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Generates a minimal PDF containing two text box fields: CustomerID and CustomerName.
    /// The method writes the file to <paramref name="path"/> so that subsequent code can load it.
    /// </summary>
    private static void CreatePdfFormTemplate(string path)
    {
        // Create a new empty PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define a rectangle for the CustomerID field
        Aspose.Pdf.Rectangle rectCustomerId = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
        TextBoxField txtCustomerId = new TextBoxField(page, rectCustomerId)
        {
            PartialName = "CustomerID",
            Value = "" // initial empty value
        };
        // Add the field to the document's form collection (not directly to page annotations)
        doc.Form.Add(txtCustomerId);

        // Define a rectangle for the CustomerName field
        Aspose.Pdf.Rectangle rectCustomerName = new Aspose.Pdf.Rectangle(100, 650, 300, 670);
        TextBoxField txtCustomerName = new TextBoxField(page, rectCustomerName)
        {
            PartialName = "CustomerName",
            Value = ""
        };
        // Add the field to the document's form collection
        doc.Form.Add(txtCustomerName);

        // Optionally add some labels so the PDF looks nicer (not required for functionality)
        page.Paragraphs.Add(new TextFragment("Customer ID:") { Position = new Position(50, 710) });
        page.Paragraphs.Add(new TextFragment("Customer Name:") { Position = new Position(50, 660) });

        // Save the template to disk
        doc.Save(path);
    }
}
