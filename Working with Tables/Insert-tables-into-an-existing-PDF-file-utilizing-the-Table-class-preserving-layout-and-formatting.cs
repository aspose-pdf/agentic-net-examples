using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTableExample
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Create a new Table instance (lifecycle rule: create)
            Table table = new Table();

            // Set basic table formatting
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            table.BackgroundColor = Aspose.Pdf.Color.LightGray;
            table.Alignment = HorizontalAlignment.Center;

            // Define column widths (optional)
            table.ColumnWidths = "100 150 100";

            // ----- Add header row -----
            Row headerRow = table.Rows.Add();
            // Header cells
            Cell headerCell1 = headerRow.Cells.Add();
            headerCell1.BackgroundColor = Aspose.Pdf.Color.DarkGray;
            headerCell1.Paragraphs.Add(new TextFragment("Product") { TextState = { FontSize = 12, FontStyle = FontStyles.Bold, ForegroundColor = Aspose.Pdf.Color.White } });

            Cell headerCell2 = headerRow.Cells.Add();
            headerCell2.BackgroundColor = Aspose.Pdf.Color.DarkGray;
            headerCell2.Paragraphs.Add(new TextFragment("Quantity") { TextState = { FontSize = 12, FontStyle = FontStyles.Bold, ForegroundColor = Aspose.Pdf.Color.White } });

            Cell headerCell3 = headerRow.Cells.Add();
            headerCell3.BackgroundColor = Aspose.Pdf.Color.DarkGray;
            headerCell3.Paragraphs.Add(new TextFragment("Price") { TextState = { FontSize = 12, FontStyle = FontStyles.Bold, ForegroundColor = Aspose.Pdf.Color.White } });

            // ----- Add data rows -----
            string[,] data = {
                { "Widget A", "10", "$5.00" },
                { "Widget B", "7",  "$7.50" },
                { "Widget C", "15", "$3.20" }
            };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row dataRow = table.Rows.Add();
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Cell cell = dataRow.Cells.Add();
                    cell.Paragraphs.Add(new TextFragment(data[i, j]) { TextState = { FontSize = 11, ForegroundColor = Aspose.Pdf.Color.Black } });
                }
            }

            // Position the table on the page using a rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle tableRect = new Aspose.Pdf.Rectangle(50, 500, 550, 700);
            // Table.Left and Table.Top expect float values – cast from double
            table.Left = (float)tableRect.LLX;
            table.Top  = (float)tableRect.URY;

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and PDF saved to '{outputPath}'.");
    }
}
