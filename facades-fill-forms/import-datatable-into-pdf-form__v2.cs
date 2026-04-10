using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF form template (must contain AcroForm fields)
        const string templatePath = "template.pdf";

        // Path where the filled PDF will be saved
        const string outputPath = "filled.pdf";

        // Verify that the template file exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // ------------------------------------------------------------
        // Build a DataTable whose column names exactly match the field
        // names in the PDF form (case‑sensitive).
        // ------------------------------------------------------------
        DataTable formData = new DataTable("FormData");
        formData.Columns.Add("FirstName", typeof(string));
        formData.Columns.Add("LastName",  typeof(string));
        formData.Columns.Add("Address",   typeof(string));

        // Populate the DataTable with one row of data.
        DataRow row = formData.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Address"]   = "123 Main St.";
        formData.Rows.Add(row);

        // ------------------------------------------------------------
        // Use AutoFiller to bind the template, import the DataTable,
        // and save the resulting PDF.
        // ------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF template file.
            autoFiller.BindPdf(templatePath);

            // Import the DataTable – each column name is mapped to a
            // field with the same name in the PDF.
            autoFiller.ImportDataTable(formData);

            // Save the filled PDF to the specified output file.
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}