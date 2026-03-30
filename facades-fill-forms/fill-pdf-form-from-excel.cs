using System;
using System.IO;
using System.Data;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: FillPdfFormFromCsv <pdfPath> <csvPath>");
            return;
        }

        string pdfPath = args[0];
        string csvPath = args[1];
        string outputPath = "filled_output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load the CSV data into a DataTable (no OleDb dependency)
        DataTable dataTable = LoadCsvToDataTable(csvPath);
        if (dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("CSV file contains no data.");
            return;
        }

        DataRow firstRow = dataTable.Rows[0];

        using (Form pdfForm = new Form())
        {
            pdfForm.BindPdf(pdfPath);
            foreach (DataColumn column in dataTable.Columns)
            {
                string fieldName = column.ColumnName;
                object valueObj = firstRow[column];
                string value = valueObj?.ToString() ?? string.Empty;
                pdfForm.FillField(fieldName, value);
            }
            pdfForm.Save(outputPath);
        }

        Console.WriteLine($"Form filled and saved to {outputPath}");
    }

    /// <summary>
    /// Reads a simple CSV file (comma‑separated, first line contains headers) into a DataTable.
    /// This replaces the Windows‑only System.Data.OleDb approach and works on all .NET platforms.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var dt = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
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
                        dt.Columns.Add(header.Trim());
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
}