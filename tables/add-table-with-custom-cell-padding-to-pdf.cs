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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new table and set the default cell padding to 6 points (MarginInfo)
            Table table = new Table
            {
                DefaultCellPadding = new MarginInfo(6, 6, 6, 6)
            };

            // Add a simple row with two cells as an example
            Row row = table.Rows.Add();
            row.Cells.Add("Header 1");
            row.Cells.Add("Header 2");

            // Add another row with data cells
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Value A");
            dataRow.Cells.Add("Value B");

            // Insert the table into the first page's paragraph collection
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
