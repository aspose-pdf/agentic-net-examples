using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public class BatchFillExample
{
    public static void Main()
    {
        // Resolve the full path to the PDF template that contains form fields named "Name" and "Address".
        // Using the application base directory makes the code work regardless of the current working directory.
        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template.pdf");

        // Verify that the template file exists before proceeding.
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Error: Template file not found at '{templatePath}'. Please ensure the file exists.");
            return;
        }

        // Output folder (current directory). Ensure the folder exists.
        string outputPath = AppDomain.CurrentDomain.BaseDirectory;
        Directory.CreateDirectory(outputPath);

        // Prepare a list of DataTable objects, each representing a different data set.
        List<DataTable> dataSets = new List<DataTable>();

        // First data set.
        DataTable table1 = new DataTable("DataSet1");
        table1.Columns.Add("Name", typeof(string));
        table1.Columns.Add("Address", typeof(string));
        DataRow row1 = table1.NewRow();
        row1["Name"] = "John Doe";
        row1["Address"] = "123 Main St";
        table1.Rows.Add(row1);
        dataSets.Add(table1);

        // Second data set.
        DataTable table2 = new DataTable("DataSet2");
        table2.Columns.Add("Name", typeof(string));
        table2.Columns.Add("Address", typeof(string));
        DataRow row2 = table2.NewRow();
        row2["Name"] = "Jane Smith";
        row2["Address"] = "456 Oak Ave";
        table2.Rows.Add(row2);
        dataSets.Add(table2);

        // Process each DataTable and generate a separate PDF file.
        for (int index = 0; index < dataSets.Count; index++)
        {
            using (AutoFiller filler = new AutoFiller())
            {
                // Initialize the facade with the template PDF.
                filler.BindPdf(templatePath);

                // Import the current data set.
                filler.ImportDataTable(dataSets[index]);

                // Build the full output file name.
                string outputFile = Path.Combine(outputPath, $"filled_{index}.pdf");

                // Save the filled PDF to the destination path.
                filler.Save(outputFile);
            }
        }

        Console.WriteLine("Batch filling completed.");
    }
}
