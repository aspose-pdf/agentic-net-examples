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
            // Create a new table
            Table table = new Table
            {
                // Adjust columns to fit the page width
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
            };

            // ----- Header row with merged cells -----
            Row headerRow = table.Rows.Add();                     // Add a new row
            Cell headerCell = headerRow.Cells.Add("Merged Header"); // Add a cell with header text
            headerCell.ColSpan = 2;                               // Merge this cell across two columns
            headerCell.BackgroundColor = Color.LightGray;         // Optional styling

            // ----- Second row (regular cells) -----
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Column 1");
            dataRow.Cells.Add("Column 2");

            // Add the table to the first page's paragraph collection
            // Pages are 1‑based in Aspose.Pdf
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}