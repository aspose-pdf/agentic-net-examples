using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // Added for TextState, FontStyles, FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "landscape_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the first page (or any target page) is set to landscape orientation
            if (doc.Pages.Count >= 1)
            {
                doc.Pages[1].PageInfo.IsLandscape = true;
            }

            // Create a wide table that will fit the landscape page
            Table table = new Table
            {
                BackgroundColor = Aspose.Pdf.Color.LightGray,
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (percentage of page width). Adjust as needed.
            table.ColumnWidths = "33 33 34";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Column A");
            header.Cells.Add("Column B");
            header.Cells.Add("Column C");
            // Make header cells bold
            foreach (Cell cell in header.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    Font = FontRepository.FindFont("Helvetica")
                };
            }

            // Add sample data rows
            for (int i = 0; i < 20; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i + 1} - A");
                row.Cells.Add($"Row {i + 1} - B");
                row.Cells.Add($"Row {i + 1} - C");
            }

            // Add the table to the first page's paragraphs collection
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Landscape PDF with table saved to '{outputPath}'.");
    }
}
