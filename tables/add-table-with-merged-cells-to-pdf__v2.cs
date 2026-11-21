using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for TextFragment
using Aspose.Pdf;        // core PDF types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table with two columns
            Table table = new Table
            {
                ColumnWidths = "200 200",                     // set column widths
                Border        = new BorderInfo(BorderSide.All, 0.5f) // thin border around cells
            };

            // ----- Row 1: a cell that spans both columns -----
            Row row1 = table.Rows.Add();                     // add a new row
            Cell mergedCell = row1.Cells.Add();              // add the first cell
            mergedCell.ColSpan = 2;                          // merge across two columns
            mergedCell.BackgroundColor = Aspose.Pdf.Color.LightGray; // optional styling
            mergedCell.Paragraphs.Add(new TextFragment("Merged cell spanning two columns"));

            // ----- Row 2: regular two cells -----
            Row row2 = table.Rows.Add();                     // second row
            Cell cell1 = row2.Cells.Add();
            cell1.Paragraphs.Add(new TextFragment("Cell 1"));
            Cell cell2 = row2.Cells.Add();
            cell2.Paragraphs.Add(new TextFragment("Cell 2"));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}