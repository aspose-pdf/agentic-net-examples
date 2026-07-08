using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF (or use a blank PDF)
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1]; // page indexing is 1‑based (rule)

            // Create a table
            Table table = new Table
            {
                // Position the table on the page
                Left = 50,
                Top  = 500,
                // Set overall table border using the proper BorderInfo constructor
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                // Optional: set background for the whole table
                BackgroundColor = Aspose.Pdf.Color.LightGray,
                // Define column widths as a space‑separated string (ColumnWidths is a string property)
                ColumnWidths = "150 150 150"
            };

            // Add header row
            Row header = new Row();
            header.BackgroundColor = Aspose.Pdf.Color.Gray;
            header.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Aspose.Pdf.Color.White
            };
            // Header cells
            foreach (string col in new[] { "Column A", "Column B", "Column C" })
            {
                Cell cell = new Cell();
                cell.Paragraphs.Add(new TextFragment(col));
                header.Cells.Add(cell);
            }
            table.Rows.Add(header);

            // Add data rows with alternating colors
            for (int i = 0; i < 6; i++)
            {
                Row row = new Row();

                // Alternate background color
                row.BackgroundColor = (i % 2 == 0)
                    ? Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9)   // light gray
                    : Aspose.Pdf.Color.White;

                // Optional: set default cell text state
                row.DefaultCellTextState = new TextState
                {
                    FontSize = 10,
                    ForegroundColor = Aspose.Pdf.Color.Black
                };

                // Add three cells per row
                for (int j = 0; j < 3; j++)
                {
                    Cell cell = new Cell();
                    cell.Paragraphs.Add(new TextFragment($"R{i + 1}C{j + 1}"));
                    row.Cells.Add(cell);
                }

                table.Rows.Add(row);
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}
