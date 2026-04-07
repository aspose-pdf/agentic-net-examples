using System;
using System.IO;
using System.Data;
using Aspose.Pdf.Facades;

class Program
{
    // Loads a CSV file into a DataTable.
    // First line is treated as column headers.
    static DataTable LoadCsvToDataTable(string csvPath)
    {
        var dt = new DataTable();
        using (var reader = new StreamReader(csvPath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var values = line.Split(',');

                if (isFirstLine)
                {
                    foreach (var header in values)
                        dt.Columns.Add(header.Trim(), typeof(string));
                    isFirstLine = false;
                }
                else
                {
                    var row = dt.NewRow();
                    for (int i = 0; i < values.Length && i < dt.Columns.Count; i++)
                        row[i] = values[i].Trim();
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }

    static void Main()
    {
        const string templatePdfPath = "template.pdf";   // PDF form template
        const string csvDataPath     = "data.csv";       // CSV containing data rows
        const string outputFolder    = "GeneratedForms"; // Folder for generated PDFs

        // Validate input files
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(csvDataPath))
        {
            Console.Error.WriteLine($"CSV data file not found: {csvDataPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load data from CSV into a DataTable
        DataTable dataTable = LoadCsvToDataTable(csvDataPath);

        // Use AutoFiller to merge each DataTable row into a separate PDF
        using (AutoFiller filler = new AutoFiller())
        {
            // Bind the PDF template
            filler.BindPdf(templatePdfPath);

            // Configure output: many small files in the specified folder
            filler.GeneratingPath = outputFolder + Path.DirectorySeparatorChar;
            filler.BasicFileName = "Form"; // Files will be Form0.pdf, Form1.pdf, ...

            // Import the data; column names must match field names in the template
            filler.ImportDataTable(dataTable);

            // Save generates one PDF per record
            filler.Save();
        }

        Console.WriteLine("Batch PDF form generation completed.");
    }
}