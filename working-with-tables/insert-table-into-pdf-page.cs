using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTableExample
{
    static void Main()
    {
        // Input PDF path, output PDF path, and the page where the table will be placed
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    pageNumber = 1;          // 1‑based page index
        const float  tableLeft   = 100f;      // X coordinate (points) – float as required by Table.Left
        const float  tableTop    = 500f;      // Y coordinate (points) – float as required by Table.Top

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a new Table instance and set its position using float values
            Table table = new Table
            {
                Left = tableLeft,
                Top  = tableTop,
                ColumnWidths = "150 150"
            };

            // Build the first row (header)
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell headerCell2 = headerRow.Cells.Add();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));

            // Build the second row (data)
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = dataRow.Cells.Add();
            dataCell1.Paragraphs.Add(new TextFragment("Data 1"));
            Cell dataCell2 = dataRow.Cells.Add();
            dataCell2.Paragraphs.Add(new TextFragment("Data 2"));

            // Retrieve the target page (1‑based indexing)
            Page page = doc.Pages[pageNumber];

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and document saved to '{outputPath}'.");
    }
}