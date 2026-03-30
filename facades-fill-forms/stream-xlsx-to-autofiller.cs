using System;
using System.IO;
using System.Data;
using Aspose.Pdf.Facades;

class Program
{
    // Adjust these paths as needed.
    private const string CsvPath = "input.csv";          // CSV version of the Excel data
    private const string TemplatePdfPath = "template.pdf";
    private const int BatchSize = 1000;

    static void Main()
    {
        if (!File.Exists(CsvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {CsvPath}");
            return;
        }
        if (!File.Exists(TemplatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {TemplatePdfPath}");
            return;
        }

        // Stream the CSV file line‑by‑line, building batches of DataTable rows.
        using (var reader = new StreamReader(CsvPath))
        {
            // Read header line to create the DataTable schema.
            string headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                Console.Error.WriteLine("CSV file is empty.");
                return;
            }
            string[] headers = headerLine.Split(',');
            DataTable batchTable = CreateBatchTable(headers);

            int batchIndex = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] values = line.Split(',');
                DataRow row = batchTable.NewRow();
                for (int i = 0; i < headers.Length && i < values.Length; i++)
                {
                    row[i] = values[i].Trim();
                }
                batchTable.Rows.Add(row);

                if (batchTable.Rows.Count >= BatchSize)
                {
                    ProcessBatch(batchTable, TemplatePdfPath, batchIndex);
                    batchTable.Clear();
                    batchIndex++;
                }
            }

            // Process any remaining rows.
            if (batchTable.Rows.Count > 0)
            {
                ProcessBatch(batchTable, TemplatePdfPath, batchIndex);
            }
        }
    }

    /// <summary>
    /// Creates a DataTable with string columns based on the CSV header row.
    /// </summary>
    private static DataTable CreateBatchTable(string[] headers)
    {
        var table = new DataTable("BatchTable");
        foreach (var header in headers)
        {
            table.Columns.Add(header.Trim(), typeof(string));
        }
        return table;
    }

    private static void ProcessBatch(DataTable table, string templatePath, int batchIndex)
    {
        // AutoFiller implements IDisposable – use a using block to guarantee cleanup.
        using (var autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePath);
            autoFiller.ImportDataTable(table);

            string outputFileName = $"output_batch_{batchIndex}.pdf";
            autoFiller.Save(outputFileName);
            Console.WriteLine($"Batch {batchIndex} saved to '{outputFileName}'.");
        }
    }
}
