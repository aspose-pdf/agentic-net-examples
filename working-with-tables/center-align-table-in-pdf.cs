using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "centered_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table();

            // Align the whole table to the center of the page
            table.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;

            // Define column widths (example: two columns of equal width)
            table.ColumnWidths = "200 200";

            // Add a single row with two cells
            Row row = new Row();
            Cell cell1 = new Cell();
            cell1.Paragraphs.Add(new TextFragment("Cell 1"));
            Cell cell2 = new Cell();
            cell2.Paragraphs.Add(new TextFragment("Cell 2"));
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            table.Rows.Add(row);

            // Add the table to the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Centered table saved to '{outputPath}'.");
    }
}