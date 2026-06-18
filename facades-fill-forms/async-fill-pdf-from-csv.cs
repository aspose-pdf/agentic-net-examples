using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfFiller
{
    // Asynchronously reads a CSV file (used in place of XLSX), fills a PDF template with its data, and saves the result.
    public static async Task FillPdfAsync(string csvPath, string pdfTemplatePath, string outputPdfPath, CancellationToken cancellationToken = default)
    {
        // ---------- 1. Load CSV data into a DataTable asynchronously ----------
        DataTable dataTable = await LoadCsvToDataTableAsync(csvPath, cancellationToken).ConfigureAwait(false);

        // ---------- 2. Load PDF template ----------
        // Document implements IDisposable, so wrap it in a using block.
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // ---------- 3. Fill PDF fields using the Form facade ----------
            // The Form class works directly on the Document instance.
            using (Form form = new Form(pdfDoc))
            {
                // Iterate over each row and fill corresponding fields.
                // This example assumes column names match PDF field names.
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        string fieldName = col.ColumnName; // PDF field name
                        string fieldValue = row[col]?.ToString() ?? string.Empty;
                        form.FillField(fieldName, fieldValue);
                    }
                }
            }

            // ---------- 4. Save the filled PDF asynchronously ----------
            // SaveAsync writes the document without blocking the calling thread.
            await pdfDoc.SaveAsync(outputPdfPath, cancellationToken).ConfigureAwait(false);
        }
    }

    // Reads a CSV file line‑by‑line asynchronously and builds a DataTable.
    private static async Task<DataTable> LoadCsvToDataTableAsync(string csvFilePath, CancellationToken cancellationToken)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            // Read header line
            string headerLine = await reader.ReadLineAsync().ConfigureAwait(false);
            if (headerLine == null)
                throw new InvalidOperationException("CSV file is empty.");

            var headers = headerLine.Split(',');
            foreach (var header in headers)
            {
                // Trim spaces and use string type for simplicity
                table.Columns.Add(header.Trim(), typeof(string));
            }

            // Read data rows
            while (!reader.EndOfStream)
            {
                // Respect cancellation token between reads
                cancellationToken.ThrowIfCancellationRequested();
                string line = await reader.ReadLineAsync().ConfigureAwait(false);
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

    // Example entry point.
    static async Task Main()
    {
        string csvPath = "data.csv";            // Source CSV file (used instead of XLSX)
        string pdfTemplatePath = "template.pdf"; // PDF template with form fields
        string outputPdfPath = "filled.pdf";     // Destination PDF

        try
        {
            await FillPdfAsync(csvPath, pdfTemplatePath, outputPdfPath);
            Console.WriteLine($"PDF successfully saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
