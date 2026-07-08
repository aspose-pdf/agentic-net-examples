using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "offset_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table();

            // Offset the table from the left margin by 50 points
            table.Margin = new MarginInfo { Left = 50 };

            // Add a simple row with two cells for demonstration
            Row row = new Row();
            row.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell 1") } });
            row.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell 2") } });
            table.Rows.Add(row);

            // Add the table to the first page (pages are 1‑based)
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table offset applied and saved to '{outputPath}'.");
    }
}
