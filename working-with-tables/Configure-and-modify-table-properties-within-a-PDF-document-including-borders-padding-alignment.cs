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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Create a new table and add it to the first page
            Table table = new Table();

            // Position the table on the page
            table.Left = 50;
            table.Top = 700;

            // Set table alignment (centered horizontally)
            table.Alignment = HorizontalAlignment.Center;

            // Configure table border (use BorderInfo for tables)
            table.Border = new BorderInfo(BorderSide.All, 1f, Color.Black);

            // Set default cell border (applies to all cells unless overridden)
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray);

            // Set default cell padding (spacing inside each cell) using MarginInfo
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5); // left, right, top, bottom

            // Set background color for the whole table (optional)
            table.BackgroundColor = Color.LightGray;

            // Define column widths (space‑separated string)
            table.ColumnWidths = "150 150 150";

            // Add a header row
            Row headerRow = table.Rows.Add();
            headerRow.DefaultCellBorder = table.DefaultCellBorder; // inherit border
            headerRow.DefaultCellPadding = table.DefaultCellPadding; // inherit padding

            // Header cells
            Cell headerCell1 = new Cell();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            headerCell1.BackgroundColor = Color.DarkBlue;
            headerCell1.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica"),
                ForegroundColor = Color.White
            };
            headerRow.Cells.Add(headerCell1);

            Cell headerCell2 = new Cell();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));
            headerCell2.BackgroundColor = Color.DarkBlue;
            headerCell2.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica"),
                ForegroundColor = Color.White
            };
            headerRow.Cells.Add(headerCell2);

            Cell headerCell3 = new Cell();
            headerCell3.Paragraphs.Add(new TextFragment("Header 3"));
            headerCell3.BackgroundColor = Color.DarkBlue;
            headerCell3.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica"),
                ForegroundColor = Color.White
            };
            headerRow.Cells.Add(headerCell3);

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.DefaultCellBorder = table.DefaultCellBorder;
            dataRow.DefaultCellPadding = table.DefaultCellPadding;

            Cell dataCell1 = new Cell();
            dataCell1.Paragraphs.Add(new TextFragment("Row 1, Col 1"));
            dataRow.Cells.Add(dataCell1);

            Cell dataCell2 = new Cell();
            dataCell2.Paragraphs.Add(new TextFragment("Row 1, Col 2"));
            dataRow.Cells.Add(dataCell2);

            Cell dataCell3 = new Cell();
            dataCell3.Paragraphs.Add(new TextFragment("Row 1, Col 3"));
            dataRow.Cells.Add(dataCell3);

            // Add the table to the page's paragraph collection
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table configured and saved to '{outputPath}'.");
    }
}
