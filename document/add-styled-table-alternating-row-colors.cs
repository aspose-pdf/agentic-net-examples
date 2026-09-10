using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for FontRepository, FontStyles, TextState

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to host the table
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table
            Table table = new Table();

            // Define three equal-width columns
            table.ColumnWidths = "100 100 100";

            // Style the table border using the BorderInfo constructor
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // Style the default cell border using the BorderInfo constructor
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);

            // Add some padding inside cells
            table.DefaultCellPadding = new MarginInfo
            {
                Top = 5,
                Bottom = 5,
                Left = 5,
                Right = 5
            };

            // ----- Header row -----
            Row header = new Row
            {
                // Header background
                BackgroundColor = Aspose.Pdf.Color.LightBlue,
                // Header text style
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Aspose.Pdf.Color.White
                }
            };
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            table.Rows.Add(header);

            // ----- Data rows with alternating background colors -----
            for (int i = 0; i < 5; i++)
            {
                Row row = new Row
                {
                    // Alternate row background: white, then light gray
                    BackgroundColor = (i % 2 == 0) ? Aspose.Pdf.Color.White : Aspose.Pdf.Color.LightGray
                };
                row.Cells.Add($"Row {i + 1} Col 1");
                row.Cells.Add($"Row {i + 1} Col 2");
                row.Cells.Add($"Row {i + 1} Col 3");
                table.Rows.Add(row);
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}
