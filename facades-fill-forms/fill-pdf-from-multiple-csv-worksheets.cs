using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvFolder = "SheetsCsv";          // Folder that contains one CSV per worksheet
        const string templatePdf = "template.pdf";
        const string outputDir = "Output";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Ensure the CSV folder exists – if it does not, create it and exit gracefully
        if (!Directory.Exists(csvFolder))
        {
            Console.WriteLine($"The folder '{csvFolder}' does not exist. Creating it now. Place your CSV files there and re‑run the program.");
            Directory.CreateDirectory(csvFolder);
            return; // Nothing to process yet
        }

        // Process each CSV file (each representing a worksheet)
        foreach (string csvPath in Directory.GetFiles(csvFolder, "*.csv"))
        {
            // Load the CSV into a DataTable
            DataTable dataTable = LoadCsvToDataTable(csvPath);

            // Use AutoFiller to merge the DataTable into the PDF template
            using (AutoFiller autoFiller = new AutoFiller())
            {
                autoFiller.BindPdf(templatePdf);
                autoFiller.ImportDataTable(dataTable);

                // Generate a distinct output file name per worksheet (based on CSV file name)
                string sheetName = Path.GetFileNameWithoutExtension(csvPath);
                string outputPath = Path.Combine(
                    outputDir,
                    $"{Path.GetFileNameWithoutExtension(templatePdf) ?? string.Empty}_{sheetName}.pdf");

                // Save the filled PDF
                autoFiller.Save(outputPath);
                Console.WriteLine($"Created PDF: {outputPath}");
            }
        }
    }

    /// <summary>
    /// Loads a CSV file into a DataTable. The first line is treated as the header row.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            // If the file is empty, return an empty table
            if (reader.EndOfStream)
                return table;

            // Read header line – guard against a possible null return
            string? headerLine = reader.ReadLine();
            if (string.IsNullOrEmpty(headerLine))
                return table; // no header, treat as empty

            var headers = headerLine.Split(',');
            foreach (var header in headers)
            {
                // Trim spaces and use string type for simplicity
                table.Columns.Add(header.Trim(), typeof(string));
            }

            // Read data rows
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                var values = line.Split(',');
                var row = table.NewRow();
                for (int i = 0; i < headers.Length && i < values.Length; i++)
                {
                    row[i] = values[i].Trim();
                }
                table.Rows.Add(row);
            }
        }
        return table;
    }
}
