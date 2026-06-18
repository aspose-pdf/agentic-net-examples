using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Loads a CSV file into a DataTable. The first line is treated as the header.
    private static DataTable LoadCsvToDataTable(string csvPath)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(csvPath))
        {
            // Read header line
            if (reader.EndOfStream)
                throw new InvalidOperationException($"CSV file '{csvPath}' is empty.");

            var headerLine = reader.ReadLine();
            var headers = headerLine.Split(',');
            foreach (var header in headers)
                table.Columns.Add(header.Trim(), typeof(string));

            // Read data rows
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                var values = line.Split(',');
                var row = table.NewRow();
                for (int i = 0; i < headers.Length && i < values.Length; i++)
                    row[i] = values[i].Trim();
                table.Rows.Add(row);
            }
        }
        return table;
    }

    static void Main()
    {
        // ---------------------------------------------------------------------
        // Configuration – adjust these paths to match your environment
        // ---------------------------------------------------------------------
        const string csvFolder   = "InputCsvs";      // Folder that contains one CSV per worksheet
        const string pdfTemplate = "template.pdf";   // PDF form template
        const string outputDir   = "GeneratedPdfs"; // Where the filled PDFs will be written

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify that the template exists
        if (!File.Exists(pdfTemplate))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplate}");
            return;
        }

        // Process each CSV file – each file represents a separate worksheet
        foreach (var csvPath in Directory.GetFiles(csvFolder, "*.csv"))
        {
            // Derive a clean name for the worksheet/PDF from the file name
            var sheetName = Path.GetFileNameWithoutExtension(csvPath);

            // Load the CSV data into a DataTable
            DataTable dataTable;
            try
            {
                dataTable = LoadCsvToDataTable(csvPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load CSV '{csvPath}': {ex.Message}");
                continue;
            }

            // Define the output PDF path for this worksheet
            var outputPdf = Path.Combine(outputDir, $"{sheetName}.pdf");

            // Use Aspose.Pdf.Facades.AutoFiller to bind data to the PDF template
            using (var autoFiller = new AutoFiller())
            {
                // Bind the PDF template
                autoFiller.InputFileName = pdfTemplate;
                // Set the output file name
                autoFiller.OutputFileName = outputPdf;
                // Import the DataTable – column names must match PDF field names (case‑sensitive)
                autoFiller.ImportDataTable(dataTable);
                // Generate the filled PDF
                autoFiller.Save();
            }

            Console.WriteLine($"Generated PDF for worksheet '{sheetName}' at '{outputPdf}'.");
        }
    }
}
