using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new page where the table will be placed
            Page page = doc.Pages.Add();

            // Create a Table and configure basic appearance
            Table table = new Table
            {
                // Define column widths (in points). Adjust as needed.
                ColumnWidths = "100 150 200",
                // Add a thin border around the whole table
                Border = new BorderInfo(BorderSide.All, 0.5f),
                // Default border for cells (optional, can also be set per‑cell)
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // ---------- First Row (Header) ----------
            Row headerRow = new Row();

            // Cell 1
            Cell headerCell1 = new Cell();
            headerCell1.Border = new BorderInfo(BorderSide.All, 0.5f);
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            headerRow.Cells.Add(headerCell1);

            // Cell 2
            Cell headerCell2 = new Cell();
            headerCell2.Border = new BorderInfo(BorderSide.All, 0.5f);
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));
            headerRow.Cells.Add(headerCell2);

            // Cell 3
            Cell headerCell3 = new Cell();
            headerCell3.Border = new BorderInfo(BorderSide.All, 0.5f);
            headerCell3.Paragraphs.Add(new TextFragment("Header 3"));
            headerRow.Cells.Add(headerCell3);

            // Attach the header row to the table
            table.Rows.Add(headerRow);

            // ---------- Second Row (Data) ----------
            Row dataRow = new Row();

            // Cell 1
            Cell dataCell1 = new Cell();
            dataCell1.Border = new BorderInfo(BorderSide.All, 0.5f);
            dataCell1.Paragraphs.Add(new TextFragment("Row1 Col1"));
            dataRow.Cells.Add(dataCell1);

            // Cell 2
            Cell dataCell2 = new Cell();
            dataCell2.Border = new BorderInfo(BorderSide.All, 0.5f);
            dataCell2.Paragraphs.Add(new TextFragment("Row1 Col2"));
            dataRow.Cells.Add(dataCell2);

            // Cell 3
            Cell dataCell3 = new Cell();
            dataCell3.Border = new BorderInfo(BorderSide.All, 0.5f);
            dataCell3.Paragraphs.Add(new TextFragment("Row1 Col3"));
            dataRow.Cells.Add(dataCell3);

            // Attach the data row to the table
            table.Rows.Add(dataRow);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}
