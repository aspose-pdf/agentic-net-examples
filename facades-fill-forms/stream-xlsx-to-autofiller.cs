using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string csvPath = "large_data.csv"; // CSV version of the Excel data
        const string templatePdfPath = "template.pdf";
        const string outputFolder = "GeneratedPdfs";
        const int batchSize = 1000;

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Open the CSV file and stream rows in batches.
        using (var reader = new StreamReader(csvPath))
        {
            // Read header line to build the DataTable schema.
            if (reader.EndOfStream)
            {
                Console.Error.WriteLine("CSV file is empty.");
                return;
            }
            string headerLine = reader.ReadLine();
            var headers = headerLine.Split(',');
            DataTable batchTable = CreateBatchTable(headers);

            int currentBatch = 0;
            int rowsInBatch = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                var values = line.Split(',');
                DataRow dataRow = batchTable.NewRow();
                for (int i = 0; i < headers.Length && i < values.Length; i++)
                {
                    dataRow[i] = values[i].Trim();
                }
                batchTable.Rows.Add(dataRow);
                rowsInBatch++;

                if (rowsInBatch >= batchSize)
                {
                    ProcessBatch(batchTable, templatePdfPath, outputFolder, currentBatch);
                    batchTable.Rows.Clear();
                    rowsInBatch = 0;
                    currentBatch++;
                }
            }

            // Process any remaining rows.
            if (batchTable.Rows.Count > 0)
            {
                ProcessBatch(batchTable, templatePdfPath, outputFolder, currentBatch);
            }
        }
    }

    // Creates a DataTable with string columns based on CSV headers.
    private static DataTable CreateBatchTable(string[] headers)
    {
        var dt = new DataTable("Batch");
        foreach (var header in headers)
        {
            dt.Columns.Add(header.Trim(), typeof(string));
        }
        return dt;
    }

    private static void ProcessBatch(DataTable data, string templatePath, string outputDir, int batchIndex)
    {
        AutoFiller autoFiller = new AutoFiller();
        // Bind the PDF template.
        autoFiller.BindPdf(templatePath);
        // Set output location for the generated PDFs.
        autoFiller.GeneratingPath = outputDir + Path.DirectorySeparatorChar;
        autoFiller.BasicFileName = $"filled_batch_{batchIndex}_";
        // Import the current batch of rows.
        autoFiller.ImportDataTable(data);
        // Generate the PDFs.
        autoFiller.Save();
        // Release resources.
        autoFiller.Close();
    }
}
