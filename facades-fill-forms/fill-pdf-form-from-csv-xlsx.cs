using System;
using System.Data;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

namespace AutoFillerConsoleApp
{
    class Program
    {
        // Path to the PDF template that contains form fields.
        // Adjust this path according to your project layout.
        private const string TemplatePdfPath = "Templates/template.pdf";

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: AutoFillerConsoleApp <inputCsvOrXlsxPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Load the CSV (or CSV‑exported XLSX) into a DataTable.
            DataTable dataTable;
            using (var stream = File.OpenRead(inputPath))
            {
                dataTable = LoadCsvToDataTable(stream);
            }

            // Fill the PDF template using Aspose.Pdf.AutoFiller.
            using (var autoFiller = new AutoFiller())
            {
                autoFiller.BindPdf(TemplatePdfPath);
                autoFiller.ImportDataTable(dataTable);

                using (var outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    autoFiller.Save(outStream);
                }
            }

            Console.WriteLine($"PDF generated successfully: {outputPath}");
        }

        // Helper: parses a CSV stream into a DataTable.
        private static DataTable LoadCsvToDataTable(Stream csvStream)
        {
            var table = new DataTable();
            using (var reader = new StreamReader(csvStream, Encoding.UTF8, leaveOpen: true))
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
                        // Create columns from header line.
                        foreach (var header in values)
                        {
                            var columnName = header.Trim();
                            if (string.IsNullOrEmpty(columnName))
                                columnName = $"Column{table.Columns.Count + 1}";
                            table.Columns.Add(columnName, typeof(string));
                        }
                        isFirstLine = false;
                    }
                    else
                    {
                        var row = table.NewRow();
                        for (int i = 0; i < values.Length && i < table.Columns.Count; i++)
                        {
                            row[i] = values[i].Trim();
                        }
                        table.Rows.Add(row);
                    }
                }
            }
            // Reset stream position for potential further use.
            if (csvStream.CanSeek)
                csvStream.Position = 0;
            return table;
        }
    }
}
