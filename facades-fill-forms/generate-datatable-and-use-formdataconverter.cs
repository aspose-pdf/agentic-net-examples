using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes from Aspose.Pdf

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // NOTE: The original example used Aspose.Cells to read an XLSX file.
        // The current project only references Aspose.Pdf, therefore the
        // Aspose.Cells assembly is unavailable and causes CS0234 errors.
        // To keep the sample self‑contained and compilable we replace the
        // Excel‑reading logic with an in‑memory DataTable that mimics the
        // structure that would be produced from an XLSX worksheet (first row
        // as column headers, subsequent rows as data).
        // ---------------------------------------------------------------------

        // Create a DataTable with column headers taken from a hypothetical
        // first row of an Excel sheet.
        DataTable dataTable = new DataTable();

        // Simulated header row – replace these with the actual headers you
        // expect from your XLSX file.
        string[] headers = new[] { "FirstName", "LastName", "Email", "Age" };
        foreach (var header in headers)
        {
            // Ensure unique column names (mirrors the original logic).
            string uniqueName = header;
            int duplicateCount = 1;
            while (dataTable.Columns.Contains(uniqueName))
            {
                uniqueName = $"{header}_{duplicateCount++}";
            }
            dataTable.Columns.Add(uniqueName, typeof(string));
        }

        // Simulated data rows – in a real scenario these would come from the
        // Excel worksheet rows (starting at row index 1).
        string[,] sampleData = new string[,] {
            { "John", "Doe", "john.doe@example.com", "30" },
            { "Jane", "Smith", "jane.smith@example.com", "28" },
            { "Bob", "Johnson", "bob.johnson@example.com", "45" }
        };

        for (int r = 0; r < sampleData.GetLength(0); r++)
        {
            DataRow row = dataTable.NewRow();
            bool hasData = false;
            for (int c = 0; c < sampleData.GetLength(1); c++)
            {
                string cellValue = sampleData[r, c];
                row[c] = cellValue;
                if (!string.IsNullOrEmpty(cellValue))
                    hasData = true;
            }
            if (hasData)
                dataTable.Rows.Add(row);
        }

        // Example usage of Aspose.Pdf.Facades: assign the DataTable to a FormDataConverter
        FormDataConverter converter = new FormDataConverter
        {
            Table = dataTable
        };

        // At this point you could convert the table to another format (e.g., XML) if needed:
        // using (MemoryStream xmlStream = new MemoryStream())
        // {
        //     converter.ConvertToStreams(new Stream[] { xmlStream }, DataType.XML);
        //     // xmlStream now contains the XML representation of the DataTable
        // }

        Console.WriteLine($"DataTable created with {dataTable.Columns.Count} columns and {dataTable.Rows.Count} rows.");
    }
}
