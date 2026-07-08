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

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Prepare sample data matching field names in the template PDF
        DataTable data = new DataTable("Data");
        data.Columns.Add("Name",    typeof(string));
        data.Columns.Add("Address", typeof(string));

        DataRow row = data.NewRow();
        row["Name"]    = "John Doe";
        row["Address"] = "123 Main St";
        data.Rows.Add(row);

        // Use AutoFiller to bind the template, import data, and save the result
        using (Aspose.Pdf.Facades.AutoFiller autoFiller = new Aspose.Pdf.Facades.AutoFiller())
        {
            // Bind the template PDF file
            autoFiller.BindPdf(templatePath);

            // Import the DataTable into the template fields
            autoFiller.ImportDataTable(data);

            // Save the filled PDF to the specified output file
            autoFiller.Save(outputPath);
            // AutoFiller.Dispose() is called automatically at the end of this block
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}