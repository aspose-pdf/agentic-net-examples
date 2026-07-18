using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfFiller
{
    static void Main()
    {
        // Path to the PDF template that contains form fields.
        const string templatePath = "Template.pdf";

        // Ensure the template exists – create it on‑the‑fly if it does not.
        if (!File.Exists(templatePath))
        {
            CreateTemplate(templatePath);
        }

        // Folder where the filled PDFs will be written.
        const string outputFolder = "FilledOutputs";
        Directory.CreateDirectory(outputFolder);

        // Prepare a list of DataTable objects, each representing a data set.
        // In a real scenario these tables would be filled from a database or other source.
        List<DataTable> dataSets = GetDataTables();

        // ---------------------------------------------------------------------
        // NOTE: The original code used Aspose.Pdf.Facades.AutoFiller. That class
        // creates files in a background thread and keeps the output file open
        // until the whole batch is saved, which caused the "file is being used"
        // IOException when the same file name was generated more than once.
        //
        // The fix replaces AutoFiller with a straightforward per‑record fill
        // approach: load the template, copy the values from the DataTable into
        // the matching form fields, and immediately save the document. This
        // guarantees that each output file is closed before the next iteration
        // starts and eliminates the need for GeneratingPath / BasicFileName.
        // ---------------------------------------------------------------------

        int index = 0;
        foreach (DataTable table in dataSets)
        {
            // Load a fresh copy of the template for each data set.
            Document doc = new Document(templatePath);

            // Assume each DataTable contains exactly one row of data.
            DataRow row = table.Rows[0];

            // Fill the form fields whose names match the column names.
            foreach (DataColumn col in table.Columns)
            {
                // The Form collection can be accessed by field name.
                // Cast to TextBoxField (or other specific field types) before setting the value.
                if (doc.Form[col.ColumnName] is TextBoxField txtField)
                {
                    txtField.Value = row[col].ToString();
                }
                // Add handling for other field types here if needed (e.g., CheckBoxField).
            }

            // Build the output file name using Path.Combine to avoid missing separators.
            string outPath = Path.Combine(outputFolder, $"FilledForm{index}.pdf");
            doc.Save(outPath);
            index++;
        }

        Console.WriteLine("Batch filling completed.");
    }

    // Creates a minimal PDF template with form fields that match the column names used in the DataTables.
    private static void CreateTemplate(string path)
    {
        // Create a new PDF document.
        Document doc = new Document();
        // Add a single page.
        Page page = doc.Pages.Add();

        // Helper to add a TextBoxField.
        void AddTextBox(string fieldName, double llx, double lly, double urx, double ury)
        {
            TextBoxField txt = new TextBoxField(page, new Rectangle(llx, lly, urx, ury))
            {
                PartialName = fieldName,
                Value = string.Empty
            };
            page.Paragraphs.Add(txt);
        }

        // Position the fields (simple vertical layout).
        double left = 100, top = 700, width = 200, height = 20, gap = 30;
        AddTextBox("FirstName", left, top, left + width, top + height);
        AddTextBox("LastName", left, top - gap, left + width, top - gap + height);
        AddTextBox("Address", left, top - 2 * gap, left + width, top - 2 * gap + height);

        // Save the template.
        doc.Save(path);
    }

    // Example method that creates dummy DataTables.
    // Replace this with actual data retrieval logic.
    private static List<DataTable> GetDataTables()
    {
        var tables = new List<DataTable>();

        // First data set
        DataTable dt1 = new DataTable("FormData");
        dt1.Columns.Add("FirstName", typeof(string));
        dt1.Columns.Add("LastName", typeof(string));
        dt1.Columns.Add("Address", typeof(string));
        dt1.Rows.Add("John", "Doe", "123 Main St");
        tables.Add(dt1);

        // Second data set
        DataTable dt2 = new DataTable("FormData");
        dt2.Columns.Add("FirstName", typeof(string));
        dt2.Columns.Add("LastName", typeof(string));
        dt2.Columns.Add("Address", typeof(string));
        dt2.Rows.Add("Jane", "Smith", "456 Oak Ave");
        tables.Add(dt2);

        // Add more tables as needed...
        return tables;
    }
}
