using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Load existing PDF or create a new one if the file does not exist
        Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document();

        // Ensure there is at least one page to place the table on
        if (doc.Pages.Count == 0)
            doc.Pages.Add();

        // Create a table with two columns (each 200 points wide)
        Table table = new Table
        {
            ColumnWidths = "200 200"
        };

        // Add a row to the table. By default the row height auto‑fits the content,
        // so we do not set a fixed height. If you need to enforce a minimum height,
        // you could use row.MinRowHeight.
        Row row = table.Rows.Add();
        // row.MinRowHeight = 0; // optional – ensures true auto‑fit behavior

        // Populate cells with text. Long text will wrap and cause the row to grow.
        Cell cell1 = row.Cells.Add();
        cell1.Paragraphs.Add(new TextFragment("First cell with a long text that should wrap and cause the row to grow automatically."));

        Cell cell2 = row.Cells.Add();
        cell2.Paragraphs.Add(new TextFragment("Second cell"));

        // Add the table to the first page
        doc.Pages[1].Paragraphs.Add(table);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved with auto‑fit row to '{outputPath}'.");
    }
}
