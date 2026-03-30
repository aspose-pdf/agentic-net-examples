using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Prepare sample data for the PDF form fields
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Address", typeof(string));
        DataRow dataRow = dataTable.NewRow();
        dataRow["Name"] = "John Doe";
        dataRow["Address"] = "123 Main St";
        dataTable.Rows.Add(dataRow);

        const string templatePath = "template.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.WriteLine($"Template file '{templatePath}' not found. Ensure the file exists before running the program.");
            return;
        }

        // Use AutoFiller to fill the template and ensure it is disposed afterwards
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Initialize the facade with the PDF file using the non‑obsolete API
            autoFiller.BindPdf(templatePath);

            // Import the data table that contains the form field values
            autoFiller.ImportDataTable(dataTable);

            // Save the filled PDF
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPath}'. AutoFiller disposed.");
    }
}