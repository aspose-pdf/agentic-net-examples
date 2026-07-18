using System;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the template PDF and the output PDF
        const string templatePdfPath = "template.pdf";
        const string outputPdfPath   = "filled.pdf";

        // Ensure a template PDF with form fields exists (sandbox has no files initially)
        CreateTemplatePdf(templatePdfPath);

        // Assume the DataTable is populated elsewhere
        DataTable dataTable = GetDataTable();

        // Load the PDF form to retrieve its field names
        using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form(templatePdfPath))
        {
            // Get all field names defined in the PDF (case‑sensitive)
            string[] pdfFieldNames = pdfForm.FieldNames;

            // Adjust DataTable column names so they match the PDF field identifiers
            foreach (DataColumn column in dataTable.Columns)
            {
                // If the column name already matches a field, keep it
                bool exactMatch = pdfFieldNames.Contains(column.ColumnName);
                if (exactMatch) continue;

                // Try a case‑insensitive match
                string? matchedField = pdfFieldNames
                    .FirstOrDefault(f => string.Equals(f, column.ColumnName, StringComparison.OrdinalIgnoreCase));

                if (matchedField != null)
                {
                    // Rename the column to the exact PDF field name
                    column.ColumnName = matchedField;
                }
                // else: No matching field – ignore the column (or handle as needed)
            }

            // Use AutoFiller to bind the template, import the adjusted DataTable, and save the result
            using (AutoFiller filler = new AutoFiller())
            {
                filler.BindPdf(templatePdfPath);          // Load the template PDF
                filler.ImportDataTable(dataTable);       // Fill fields using the DataTable
                filler.Save(outputPdfPath);              // Save the filled PDF
            }
        }
    }

    // Creates a simple PDF containing form fields that correspond to the example DataTable columns.
    private static void CreateTemplatePdf(string path)
    {
        // Create a new PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Add a text box field for FirstName
        TextBoxField firstName = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 300, 720))
        {
            PartialName = "FirstName",
            Value = string.Empty
        };
        doc.Form.Add(firstName, 1);

        // Add a text box field for LastName
        TextBoxField lastName = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 650, 300, 670))
        {
            PartialName = "LastName",
            Value = string.Empty
        };
        doc.Form.Add(lastName, 1);

        // Add a text box field for Address
        TextBoxField address = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 300, 620))
        {
            PartialName = "Address",
            Value = string.Empty
        };
        doc.Form.Add(address, 1);

        // Save the template PDF so that subsequent code can load it
        doc.Save(path);
    }

    // Placeholder method to obtain a populated DataTable.
    // Replace with actual data retrieval logic.
    static DataTable GetDataTable()
    {
        DataTable dt = new DataTable("FormData");
        // Example columns (these may not match PDF field names initially)
        dt.Columns.Add("FirstName", typeof(string));
        dt.Columns.Add("LastName", typeof(string));
        dt.Columns.Add("Address", typeof(string));

        // Example row
        dt.Rows.Add("John", "Doe", "123 Main St");

        return dt;
    }
}
