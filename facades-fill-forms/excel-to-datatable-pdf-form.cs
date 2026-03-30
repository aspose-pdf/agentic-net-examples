using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string csvPath = "data.csv";               // CSV version of the Excel sheet
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine("CSV file not found: " + csvPath);
            return;
        }

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine("PDF template not found: " + pdfTemplatePath);
            return;
        }

        DataTable dataTable = LoadCsvToDataTable(csvPath);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("Failed to load CSV data or file is empty.");
            return;
        }

        // AutoFiller implements IDisposable – use a using‑block to ensure proper cleanup.
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(pdfTemplatePath);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Loads a CSV file into a DataTable. The first line is treated as column headers.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable();
        try
        {
            using (var reader = new StreamReader(csvFilePath))
            {
                bool firstLine = true;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue; // skip empty lines

                    // Simple split on commas – for more complex CSVs consider a dedicated parser.
                    string[] fields = line.Split(',');

                    if (firstLine)
                    {
                        // Create columns from the header row.
                        foreach (string header in fields)
                        {
                            string colName = header.Trim();
                            // Ensure unique column names.
                            string uniqueName = colName;
                            int duplicate = 1;
                            while (table.Columns.Contains(uniqueName))
                            {
                                uniqueName = colName + "_" + duplicate++;
                            }
                            table.Columns.Add(uniqueName, typeof(string));
                        }
                        firstLine = false;
                    }
                    else
                    {
                        // Populate a data row.
                        DataRow row = table.NewRow();
                        int colCount = Math.Min(fields.Length, table.Columns.Count);
                        for (int i = 0; i < colCount; i++)
                        {
                            row[i] = fields[i].Trim();
                        }
                        table.Rows.Add(row);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV file '{csvFilePath}': {ex.Message}");
            return null;
        }
        return table;
    }
}
