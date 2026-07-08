using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDF template and the directory that contains CSV files (one per worksheet).
        const string templatePdf = "template.pdf";
        const string csvFolder = "Sheets"; // Folder with CSV files representing each worksheet.
        const string outputDir = "Output";

        // Verify input files exist.
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"PDF template not found: {templatePdf}");
            return;
        }
        if (!Directory.Exists(csvFolder))
        {
            Console.Error.WriteLine($"CSV folder not found: {csvFolder}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Get all CSV files – each file is treated as a separate worksheet.
        string[] csvFiles = Directory.GetFiles(csvFolder, "*.csv");
        if (csvFiles.Length == 0)
        {
            Console.Error.WriteLine($"No CSV files found in folder: {csvFolder}");
            return;
        }

        foreach (string csvPath in csvFiles)
        {
            // Derive a sheet‑like name from the file name (without extension).
            string sheetName = Path.GetFileNameWithoutExtension(csvPath);

            // Load the CSV data into a DataTable.
            DataTable dataTable = LoadCsvToDataTable(csvPath);

            // Use AutoFiller to merge the DataTable with the PDF template.
            using (AutoFiller filler = new AutoFiller())
            {
                // Initialise the facade with the template PDF (new API).
                filler.BindPdf(templatePdf);

                // Import the data.
                filler.ImportDataTable(dataTable);

                // Build the output file name – e.g. "template_Sheet1.pdf".
                string outputPdf = Path.Combine(
                    outputDir,
                    $"{Path.GetFileNameWithoutExtension(templatePdf)}_{sheetName}.pdf");

                // Save the generated PDF.
                filler.Save(outputPdf);
            }
        }

        Console.WriteLine("All worksheets have been processed.");
    }

    // Simple CSV → DataTable loader.
    // Assumes the first line contains column headers and values are comma‑separated.
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable(Path.GetFileNameWithoutExtension(csvFilePath));
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] fields = line.Split(',');
                if (isFirstLine)
                {
                    // Create columns based on the header row.
                    foreach (string header in fields)
                    {
                        table.Columns.Add(header.Trim(), typeof(string));
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add a data row.
                    var row = table.NewRow();
                    for (int i = 0; i < table.Columns.Count && i < fields.Length; i++)
                    {
                        row[i] = fields[i].Trim();
                    }
                    table.Rows.Add(row);
                }
            }
        }
        return table;
    }
}
