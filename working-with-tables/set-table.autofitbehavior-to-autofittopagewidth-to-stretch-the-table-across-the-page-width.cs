using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table();

            // -----------------------------------------------------------------
            // BACKWARD COMPATIBILITY: For Aspose.Pdf versions that do not expose
            // Table.AutoFitBehavior, calculate the page width and set column widths
            // manually so the table stretches to the full page width.
            // -----------------------------------------------------------------
            float pageWidth = (float)doc.Pages[1].PageInfo.Width;
            // Divide the width equally between the two columns (adjust as needed).
            table.ColumnWidths = $"{pageWidth / 2} {pageWidth / 2}";

            // Add a header row
            var headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            // Add a data row
            var dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");

            // Insert the table into the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table stretched to page width saved to '{outputPath}'.");
    }
}
