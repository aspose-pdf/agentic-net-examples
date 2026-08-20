using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for any device-specific options if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (page-indexing-one-based rule)
            Page page = doc.Pages[1];

            // Create a new table
            Table table = new Table();

            // Offset the table from the left margin by setting Margin.Left
            // MarginInfo.Left expects a double value representing points
            table.Margin = new MarginInfo { Left = 50.0 }; // 50 points offset

            // Optionally add some content to the table for demonstration
            // (e.g., a single row with two cells)
            table.ColumnWidths = "200 200"; // two columns, each 200 points wide
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (document-disposal-with-using rule ensures proper disposal)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table offset applied and saved to '{outputPath}'.");
    }
}