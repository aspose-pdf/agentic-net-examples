using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class PdfFormFiller
{
    /// <summary>
    /// Fills an AcroForm PDF using data from the first worksheet of a CSV file (exported from Excel).
    /// Each column name in the CSV must match a fully‑qualified field name in the PDF.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF form.</param>
    /// <param name="csvPath">Path to the CSV file (exported from the XLSX workbook).</param>
    /// <param name="outputPath">Path where the filled PDF will be saved.</param>
    public static void FillForm(string pdfPath, string csvPath, string outputPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load data from the CSV file into a DataTable.
        DataTable dataTable = LoadCsvToDataTable(csvPath);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("No data found in the CSV file.");
            return;
        }

        // Use the first row of the CSV to fill the form.
        DataRow row = dataTable.Rows[0];

        // Create the Form facade and bind the source PDF.
        using (Form form = new Form(pdfPath))
        {
            // Iterate over each column and attempt to fill the corresponding field.
            foreach (DataColumn column in dataTable.Columns)
            {
                string fieldName = column.ColumnName;               // Expected to be the full field name.
                string fieldValue = row[column]?.ToString() ?? "";

                // Fill the field; ignore the boolean result (false means field not found).
                form.FillField(fieldName, fieldValue);
            }

            // Save the filled PDF to the specified output path.
            form.Save(outputPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Loads a CSV file into a DataTable. The first line is treated as the header row.
    /// This method replaces the previous OleDb‑based Excel reader, removing the Windows‑only dependency.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                var values = line.Split(','); // Simple split – works for basic CSVs without escaped commas.

                if (isFirstLine)
                {
                    // Create columns from header line.
                    foreach (var header in values)
                    {
                        table.Columns.Add(header.Trim(), typeof(string));
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
        return table;
    }

    // Example usage:
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: PdfFormFiller <inputPdf> <inputCsv> <outputPdf>");
            return;
        }

        string inputPdf = args[0];
        string inputCsv = args[1];
        string outputPdf = args[2];

        FillForm(inputPdf, inputCsv, outputPdf);
    }
}
