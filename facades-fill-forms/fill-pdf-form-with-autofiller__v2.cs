using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled_output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Prepare sample data for the form fields.
        DataTable data = new DataTable("FormData");
        data.Columns.Add("FirstName", typeof(string));
        data.Columns.Add("LastName",  typeof(string));
        data.Columns.Add("Email",     typeof(string));

        // Add a single row of data (replace with real data as needed).
        DataRow row = data.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Email"]     = "john.doe@example.com";
        data.Rows.Add(row);

        // Use a using block so AutoFiller.Dispose() is called automatically.
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the template PDF.
            autoFiller.InputFileName = templatePath;

            // Import the data table; column names must match form field names.
            autoFiller.ImportDataTable(data);

            // Save the filled PDF to the specified file.
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}