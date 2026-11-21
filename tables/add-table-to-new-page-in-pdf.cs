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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new table
            Table table = new Table
            {
                // Start the table on a fresh page
                IsInNewPage = true,

                // Optional: set column widths (percentage of page width)
                ColumnWidths = "200 200"
            };

            // First row (header)
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell headerCell2 = headerRow.Cells.Add();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));

            // Second row (data)
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = dataRow.Cells.Add();
            dataCell1.Paragraphs.Add(new TextFragment("Value A"));
            Cell dataCell2 = dataRow.Cells.Add();
            dataCell2.Paragraphs.Add(new TextFragment("Value B"));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}