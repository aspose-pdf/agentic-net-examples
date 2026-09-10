using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // NOTE: The original example used an Excel workbook. To keep the sample
        // cross‑platform and avoid the Windows‑only System.Data.OleDb provider,
        // the data is now read from CSV files – one CSV per logical worksheet.
        const string csvFolder   = "CsvData";      // folder containing CSV files (one per worksheet)
        const string templatePdf = "template.pdf"; // PDF form with fields matching column names
        const string outputDir   = "GeneratedPdfs"; // folder for the generated PDFs

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Extract a DataTable for each CSV file (treated as a worksheet)
        List<DataTable> worksheets = GetDataTablesFromCsv(csvFolder);

        // Process each worksheet independently
        for (int i = 0; i < worksheets.Count; i++)
        {
            DataTable dt = worksheets[i];

            // AutoFiller handles the mail‑merge / form‑fill operation
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the PDF template (no need for InputFileName property)
                filler.BindPdf(templatePdf);

                // Import the data for the current worksheet
                filler.ImportDataTable(dt);

                // Generate the PDF for this worksheet – use the newer Save(string) overload
                string outputPath = Path.Combine(outputDir, $"Result_{i}.pdf");
                filler.Save(outputPath);
            }
        }

        Console.WriteLine("All worksheets processed successfully.");
    }

    // Reads every CSV file from a folder and returns a list of DataTables.
    // The first line of each CSV is treated as column headers.
    private static List<DataTable> GetDataTablesFromCsv(string folderPath)
    {
        var tables = new List<DataTable>();
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"CSV folder '{folderPath}' does not exist. No data will be processed.");
            return tables;
        }

        foreach (string csvFile in Directory.GetFiles(folderPath, "*.csv"))
        {
            DataTable dt = LoadCsvToDataTable(csvFile);
            dt.TableName = Path.GetFileNameWithoutExtension(csvFile);
            tables.Add(dt);
        }
        return tables;
    }

    // Simple CSV parser that loads the content of a CSV file into a DataTable.
    // It assumes commas as delimiters and does not handle quoted commas or line breaks.
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable();
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
                    // Create columns from the header line
                    foreach (string header in fields)
                    {
                        string colName = header.Trim();
                        if (string.IsNullOrEmpty(colName))
                            colName = $"Column{table.Columns.Count}";
                        // All columns are treated as string for simplicity
                        table.Columns.Add(colName, typeof(string));
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add a data row
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
