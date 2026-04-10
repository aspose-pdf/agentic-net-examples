using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder that contains one CSV file per worksheet.
        const string csvFolder = "SheetsCsv";
        const string templatePdf = "TemplateForm.pdf";
        const string outputDir = "GeneratedPdfs";

        // Ensure required directories exist.
        Directory.CreateDirectory(outputDir);
        Directory.CreateDirectory(csvFolder);

        // Process each CSV file – each file represents a worksheet.
        foreach (var csvPath in Directory.GetFiles(csvFolder, "*.csv"))
        {
            string sheetName = Path.GetFileNameWithoutExtension(csvPath);

            // Load the CSV into a DataTable (no OleDb required).
            DataTable dataTable = LoadCsvToDataTable(csvPath);

            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine($"CSV '{sheetName}' is empty – skipping.");
                continue;
            }

            // Use AutoFiller to merge the data into the PDF template.
            using (AutoFiller autoFiller = new AutoFiller())
            {
                autoFiller.BindPdf(templatePdf);
                autoFiller.GeneratingPath = outputDir + Path.DirectorySeparatorChar;
                // BasicFileName is used as a prefix; Aspose.Pdf will append an index.
                autoFiller.BasicFileName = $"Filled_{sheetName}_";
                autoFiller.ImportDataTable(dataTable);
                autoFiller.Save();
            }

            Console.WriteLine($"Processed worksheet '{sheetName}' – PDF generated.");
        }

        Console.WriteLine("All worksheets have been processed.");
    }

    /// <summary>
    /// Reads a CSV file into a DataTable. The first line is treated as column headers.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool firstLine = true;
            string[] headers = null;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var fields = line.Split(',');
                if (firstLine)
                {
                    headers = fields;
                    foreach (var header in headers)
                        table.Columns.Add(header.Trim(), typeof(string));
                    firstLine = false;
                }
                else
                {
                    var row = table.NewRow();
                    for (int i = 0; i < headers.Length && i < fields.Length; i++)
                        row[i] = fields[i].Trim();
                    table.Rows.Add(row);
                }
            }
        }
        return table;
    }
}
