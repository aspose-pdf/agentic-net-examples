using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled_combined.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Create a synthetic DataTable whose column names exactly match the PDF form field names
        DataTable data = new DataTable();
        data.Columns.Add("FirstName");
        data.Columns.Add("LastName");
        data.Columns.Add("Email");

        // Add multiple rows of data
        data.Rows.Add("John",  "Doe",   "john.doe@example.com");
        data.Rows.Add("Jane",  "Smith", "jane.smith@example.com");
        data.Rows.Add("Bob",   "Johnson","bob.johnson@example.com");

        // AutoFiller binds the template, imports the table (case‑sensitive by default), and saves a single PDF
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePath);
            // No CaseSensitive property – mapping is case‑sensitive by default.
            filler.ImportDataTable(data);
            filler.Save(outputPath);
            // Close releases internal resources; using‑statement will also call Dispose.
            filler.Close();
        }

        Console.WriteLine($"Combined PDF saved to '{outputPath}'.");
    }
}
