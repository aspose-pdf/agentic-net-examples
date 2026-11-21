using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to host the table
            Page page = doc.Pages[1];

            // Create a new table
            Table table = new Table();

            // Optional: define column widths (example: two equal columns)
            table.ColumnWidths = "200 200";

            // Add rows to the table
            const int totalRows = 12; // example row count
            for (int i = 1; i <= totalRows; i++)
            {
                // Add a new row
                Row row = table.Rows.Add();

                // Every third row starts on a new page
                row.IsInNewPage = (i % 3 == 0);

                // Add a single cell with some text
                Cell cell = row.Cells.Add();
                cell.Paragraphs.Add(new TextFragment($"Row {i}"));
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}