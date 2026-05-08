using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string csvFolder = "SheetsCsv";          // Folder containing one CSV per worksheet
        const string pdfTemplatePath = "template.pdf"; // PDF form template
        const string outputFolder = "GeneratedPdfs";   // Folder for output PDFs

        // Ensure required directories exist
        Directory.CreateDirectory(outputFolder);
        Directory.CreateDirectory(csvFolder);

        // Process each CSV file (each represents a worksheet)
        foreach (string csvPath in Directory.GetFiles(csvFolder, "*.csv"))
        {
            // Load CSV data into a DataTable
            DataTable dataTable = LoadCsvToDataTable(csvPath);

            // Fill the PDF using AutoFiller
            using (AutoFiller autoFiller = new AutoFiller())
            {
                autoFiller.BindPdf(pdfTemplatePath);
                autoFiller.ImportDataTable(dataTable);

                string safeName = Path.GetFileNameWithoutExtension(csvPath);
                string outputPath = Path.Combine(outputFolder, $"{safeName}.pdf");
                autoFiller.Save(outputPath);
            }
        }

        Console.WriteLine("All worksheets processed and PDFs generated.");
    }

    /// <summary>
    /// Reads a CSV file and returns a DataTable. The first line is treated as column headers.
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
                if (line == null) continue;
                var fields = line.Split(',');

                if (firstLine)
                {
                    headers = fields;
                    foreach (var header in headers)
                    {
                        table.Columns.Add(header.Trim(), typeof(string));
                    }
                    firstLine = false;
                }
                else
                {
                    var row = table.NewRow();
                    for (int i = 0; i < headers.Length && i < fields.Length; i++)
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