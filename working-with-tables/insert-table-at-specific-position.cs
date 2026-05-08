using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTableAtPosition
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the table will be placed (1‑based index)
            Page page = doc.Pages[1];

            // Create a new table
            Table table = new Table();

            // Set the absolute position of the table on the page using MarginInfo.
            // X = 100 points from the left, Y = 500 points from the bottom.
            // Aspose.Pdf uses a top‑down coordinate system for margins, so we set the left margin
            // and calculate the top margin as (page height - desired Y coordinate).
            double desiredX = 100; // points from left
            double desiredYFromBottom = 500; // points from bottom
            double topMargin = page.PageInfo.Height - desiredYFromBottom; // convert to top‑based coordinate
            table.Margin = new MarginInfo { Left = desiredX, Top = topMargin };

            // Define column widths (two columns, 200 points each)
            table.ColumnWidths = "200 200";

            // Add a header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Header 1");
            Cell headerCell2 = header.Cells.Add("Header 2");
            headerCell1.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica"),
                ForegroundColor = Aspose.Pdf.Color.Black // Aspose.Pdf.Color, not System.Drawing.Color
            };
            headerCell2.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica"),
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Value A");
            dataRow.Cells.Add("Value B");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted at (100, 500) and saved to '{outputPath}'.");
    }
}
