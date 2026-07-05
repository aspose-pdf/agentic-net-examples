using System;
using System.Data;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string csvPath = "data.csv";          // Source CSV file (converted from XLSX)
        const string templatePdf = "template.pdf";   // PDF template with form fields
        const string outputDir = "Output";           // Folder for generated PDFs

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        const int batchSize = 1000; // Number of rows per AutoFiller batch
        DataTable batchTable = null;
        int totalRows = 0;
        int batchIndex = 0;

        // Stream the CSV line‑by‑line to avoid loading the whole file into memory.
        using (var reader = new StreamReader(csvPath, Encoding.UTF8))
        {
            // Read header line and create the DataTable schema.
            string headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                Console.Error.WriteLine("CSV file is empty.");
                return;
            }
            var headers = SplitCsvLine(headerLine);
            batchTable = CreateBatchTable(headers);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var values = SplitCsvLine(line);
                DataRow dr = batchTable.NewRow();
                for (int i = 0; i < headers.Length && i < values.Length; i++)
                {
                    dr[i] = string.IsNullOrEmpty(values[i]) ? DBNull.Value : (object)values[i];
                }
                batchTable.Rows.Add(dr);
                totalRows++;

                if (batchTable.Rows.Count >= batchSize)
                {
                    ProcessBatch(batchTable, templatePdf, outputDir, batchIndex);
                    batchTable.Clear();
                    batchIndex++;
                }
            }
        }

        // Process any remaining rows that didn't fill a complete batch.
        if (batchTable != null && batchTable.Rows.Count > 0)
        {
            ProcessBatch(batchTable, templatePdf, outputDir, batchIndex);
        }

        Console.WriteLine($"Processed {totalRows} rows in {batchIndex + 1} batch(es).");
    }

    /// <summary>
    /// Splits a CSV line respecting simple commas. For complex CSV (quotes, escaped commas) replace with a robust parser.
    /// </summary>
    private static string[] SplitCsvLine(string line)
    {
        // This simple split works for most straightforward CSV files.
        return line.Split(',');
    }

    /// <summary>
    /// Creates an empty DataTable with columns based on the CSV header.
    /// All columns are typed as string to keep parsing simple and memory‑efficient.
    /// </summary>
    private static DataTable CreateBatchTable(string[] headers)
    {
        var table = new DataTable("Batch");
        foreach (var h in headers)
        {
            table.Columns.Add(h.Trim(), typeof(string));
        }
        return table;
    }

    /// <summary>
    /// Processes a batch of rows using Aspose.Pdf.Facades.AutoFiller.
    /// Each row is written to a separate PDF file whose name contains the batch index and row index.
    /// </summary>
    private static void ProcessBatch(DataTable batch, string templatePdf, string outputDir, int batchIndex)
    {
        if (batch == null || batch.Rows.Count == 0)
            return;

        int rowIdx = 0;
        foreach (DataRow row in batch.Rows)
        {
            // Create a temporary DataTable that contains only the current row.
            DataTable singleRowTable = batch.Clone();
            singleRowTable.ImportRow(row);

            string outputPath = Path.Combine(outputDir, $"output_batch{batchIndex}_row{rowIdx}.pdf");

            using (AutoFiller filler = new AutoFiller())
            {
                filler.BindPdf(templatePdf);
                filler.ImportDataTable(singleRowTable);
                filler.Save(outputPath);
            }

            singleRowTable.Dispose();
            rowIdx++;
        }
    }
}
