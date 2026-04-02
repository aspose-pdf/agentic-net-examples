using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "right_aligned_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table();

            // Align the table to the right margin
            table.Alignment = HorizontalAlignment.Right;

            // Adjust the left margin (set to zero so the table hugs the right edge)
            table.Margin = new MarginInfo { Left = 0 };

            // Add a simple row and cell with some text
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextFragment("Right‑aligned table"));

            // Add the table to the first page
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table aligned to right margin saved as '{outputPath}'.");
    }
}