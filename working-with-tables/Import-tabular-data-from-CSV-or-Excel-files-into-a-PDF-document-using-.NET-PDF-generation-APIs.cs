using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextState, FontRepository, etc.

class Program
{
    static void Main()
    {
        // Input CSV file (you can replace with an Excel file path)
        const string csvPath = "data.csv";
        // Output PDF file
        const string pdfPath = "output.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV data into a DataTable
        DataTable tableData = LoadCsvIntoDataTable(csvPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf Table
            Table pdfTable = new Table();

            // Import the DataTable into the PDF table.
            // Parameters:
            //   tableData               – source DataTable
            //   true                    – import column names as the first row
            //   0                       – start importing at the first row of the PDF table (zero‑based)
            //   0                       – start importing at the first column of the PDF table (zero‑based)
            pdfTable.ImportDataTable(tableData, true, 0, 0);

            // Optional: set some visual properties
            pdfTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
            pdfTable.DefaultCellPadding = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 };
            pdfTable.DefaultCellTextState = new TextState
            {
                FontSize = 10,
                Font = FontRepository.FindFont("Helvetica")
            };
            pdfTable.Border = new BorderInfo(BorderSide.All, 1f);
            pdfTable.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Add the table to the page
            page.Paragraphs.Add(pdfTable);

            // Save the PDF document
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created successfully: {pdfPath}");
    }

    // Helper method to read a CSV file into a DataTable
    private static DataTable LoadCsvIntoDataTable(string csvFilePath)
    {
        DataTable dt = new DataTable();

        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] fields = line.Split(',');

                if (isFirstLine)
                {
                    // Use the first line as column headers
                    foreach (string header in fields)
                        dt.Columns.Add(header.Trim());
                    isFirstLine = false;
                }
                else
                {
                    // Add data rows
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < fields.Length; i++)
                        row[i] = fields[i].Trim();
                    dt.Rows.Add(row);
                }
            }
        }

        return dt;
    }
}
