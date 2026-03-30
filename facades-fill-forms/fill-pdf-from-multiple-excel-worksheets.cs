using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder that contains one CSV file per logical worksheet
        string csvFolderPath = "InputSheets"; // e.g., InputSheets/Sheet1.csv, Sheet2.csv, ...
        string templatePdfPath = "template.pdf";
        string outputDirectory = "Output";

        if (!Directory.Exists(csvFolderPath))
        {
            Console.Error.WriteLine($"CSV folder not found: {csvFolderPath}");
            return;
        }

        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Process every *.csv file in the folder – each file represents a separate worksheet
        foreach (string csvFilePath in Directory.GetFiles(csvFolderPath, "*.csv"))
        {
            // Load the CSV into a DataTable (first line = column headers)
            DataTable dataTable = LoadCsvToDataTable(csvFilePath);

            using (AutoFiller autoFiller = new AutoFiller())
            {
                autoFiller.BindPdf(templatePdfPath);
                autoFiller.ImportDataTable(dataTable);

                string safeSheetName = Path.GetFileNameWithoutExtension(csvFilePath).Replace(" ", "_");
                string outputPath = Path.Combine(outputDirectory,
                    $"{Path.GetFileNameWithoutExtension(templatePdfPath)}_{safeSheetName}.pdf");

                autoFiller.Save(outputPath);
                Console.WriteLine($"Generated PDF for CSV '{csvFilePath}': {outputPath}");
            }
        }
    }

    /// <summary>
    /// Very simple CSV parser that reads the first line as column headers and the remaining lines as rows.
    /// It splits on commas and trims surrounding whitespace. It does not handle escaped commas or quotes –
    /// sufficient for demonstration purposes and removes the need for System.Data.OleDb.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable(Path.GetFileNameWithoutExtension(csvFilePath));
        using (var reader = new StreamReader(csvFilePath))
        {
            // Read header line
            string headerLine = reader.ReadLine();
            if (headerLine == null)
                return table; // empty file

            string[] headers = headerLine.Split(',');
            foreach (string header in headers)
                table.Columns.Add(header.Trim());

            // Read data rows
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] fields = line.Split(',');
                var row = table.NewRow();
                for (int i = 0; i < table.Columns.Count && i < fields.Length; i++)
                    row[i] = fields[i].Trim();
                table.Rows.Add(row);
            }
        }
        return table;
    }
}
