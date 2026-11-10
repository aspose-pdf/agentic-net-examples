using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Aspose.Pdf.Facades; // AutoFiller is in this namespace

class Program
{
    // Entry point: args[0] = template PDF path, args[1] = data CSV path, args[2] = output PDF path
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <template.pdf> <data.csv> <output.pdf>");
            return;
        }

        string templatePdfPath = args[0];
        string dataCsvPath     = args[1];
        string outputPdfPath   = args[2];

        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        if (!File.Exists(dataCsvPath))
        {
            Console.Error.WriteLine($"Data CSV not found: {dataCsvPath}");
            return;
        }

        try
        {
            // Load CSV data into a DataTable (no OleDb usage)
            DataTable dataTable = LoadCsvToDataTable(dataCsvPath);

            // Use AutoFiller to bind the PDF template and import the data
            using (AutoFiller filler = new AutoFiller())
            {
                filler.BindPdf(templatePdfPath);
                filler.ImportDataTable(dataTable);
                filler.Save(outputPdfPath);
            }

            Console.WriteLine($"Form filled PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Reads a CSV file and returns a DataTable.
    // Assumes the first line contains column headers.
    private static DataTable LoadCsvToDataTable(string csvPath)
    {
        DataTable table = new DataTable();

        using (var reader = new StreamReader(csvPath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;

                // Simple CSV split – does not handle quoted commas.
                string[] fields = line.Split(',');

                if (isFirstLine)
                {
                    // Create columns from header row
                    foreach (string header in fields)
                    {
                        string colName = header.Trim();
                        if (string.IsNullOrEmpty(colName))
                            colName = $"Column{table.Columns.Count + 1}";
                        table.Columns.Add(colName, typeof(string));
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add data rows
                    DataRow row = table.NewRow();
                    for (int i = 0; i < fields.Length && i < table.Columns.Count; i++)
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