using System;
using System.Data;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string csvPath = "data.csv";           // DataTable source (CSV with PageNumber,Identifier columns)
        const string reportPath = "summary_report.csv"; // output report

        // Validate input files
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // Load CSV into a DataTable
        DataTable table = new DataTable();
        using (StreamReader reader = new StreamReader(csvPath))
        {
            bool headerRead = false;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(',');

                if (!headerRead)
                {
                    // First line defines column names
                    foreach (string col in parts)
                        table.Columns.Add(col.Trim());
                    headerRead = true;
                }
                else
                {
                    // Subsequent lines are data rows
                    table.Rows.Add(parts);
                }
            }
        }

        // Obtain page count using Document (PdfFileEditor does not expose GetPageCount)
        int pageCount;
        using (Document pdfDoc = new Document(pdfPath))
        {
            pageCount = pdfDoc.Pages.Count; // pages are 1‑based
        }

        // Create the summary report (CSV)
        using (StreamWriter writer = new StreamWriter(reportPath, false))
        {
            // Header
            writer.WriteLine("PageNumber,Identifier");

            // Iterate through each PDF page
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Find the DataTable row that matches the current page number
                // Assumes the DataTable has columns named "PageNumber" and "Identifier"
                DataRow[] matches = table.Select($"PageNumber = {pageNum}");

                string identifier = matches.Length > 0
                    ? matches[0]["Identifier"]?.ToString() ?? "N/A"
                    : "N/A";

                // Write the mapping to the report
                writer.WriteLine($"{pageNum},{identifier}");
            }
        }

        Console.WriteLine($"Summary report generated: {reportPath}");
    }
}
