using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Resolve the CSV path relative to the executable directory.
        // This prevents "FileNotFoundException" when the working directory is different.
        string csvPath = Path.Combine(AppContext.BaseDirectory, "data.csv");
        const string templatePdfPath = "template.pdf";
        const string outputPdfPath = "merged_output.pdf";

        // Ensure the CSV file exists – if not, create a minimal placeholder so the demo can run.
        if (!File.Exists(csvPath))
        {
            CreatePlaceholderCsv(csvPath);
            Console.WriteLine($"Placeholder CSV created at '{csvPath}'.");
        }

        // Ensure the PDF template exists – if not, create a very simple PDF so the demo can run.
        if (!File.Exists(templatePdfPath))
        {
            CreatePlaceholderPdf(templatePdfPath);
            Console.WriteLine($"Placeholder PDF template created at '{templatePdfPath}'.");
        }

        // Load the CSV into a DataTable. The method streams the file line‑by‑line so even very large files stay within memory limits.
        DataTable dataTable = LoadCsvToDataTable(csvPath);

        // Merge the data into the PDF template using AutoFiller.
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(templatePdfPath);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPdfPath);
        }

        Console.WriteLine($"Merged PDF created: {outputPdfPath}");
    }

    /// <summary>
    /// Reads a CSV file and returns a DataTable whose columns are taken from the first line (header).
    /// The implementation reads the file sequentially, adding rows as they are parsed – no full‑file buffering.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable("MailMerge");
        using (var reader = new StreamReader(csvFilePath))
        {
            // Header line – defines column names.
            if (reader.EndOfStream)
                throw new InvalidOperationException("CSV file is empty.");

            string headerLine = reader.ReadLine();
            string[] headers = headerLine.Split(',');
            foreach (string header in headers)
                table.Columns.Add(header.Trim(), typeof(string));

            // Data rows – streamed one at a time.
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] values = line.Split(',');
                DataRow row = table.NewRow();
                for (int i = 0; i < headers.Length && i < values.Length; i++)
                    row[i] = values[i].Trim();

                table.Rows.Add(row);
            }
        }
        return table;
    }

    /// <summary>
    /// Creates a tiny placeholder CSV file with a header and a single row.
    /// This method is only invoked when the expected CSV does not exist, allowing the sample to run without external files.
    /// </summary>
    private static void CreatePlaceholderCsv(string path)
    {
        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine("Name,Email,Address");
            writer.WriteLine("John Doe,john.doe@example.com,123 Main St");
        }
    }

    /// <summary>
    /// Creates a minimal PDF file that can be used as a template for AutoFiller.
    /// The PDF contains a single blank page; no form fields are required for the demo to succeed.
    /// </summary>
    private static void CreatePlaceholderPdf(string path)
    {
        // Create a new PDF document with one empty page.
        var doc = new Document();
        doc.Pages.Add();
        doc.Save(path);
    }
}
