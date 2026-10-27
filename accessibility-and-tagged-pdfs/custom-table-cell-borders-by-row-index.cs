using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_borders.pdf";

        // Ensure deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a table with 5 columns
            Table table = new Table
            {
                // Define equal column widths
                ColumnWidths = "100 100 100 100 100",
                // Default border for cells (will be overridden per row)
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.LightGray),
                // Default padding for better readability
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Add a header row
            Row header = table.Rows.Add();
            for (int c = 1; c <= 5; c++)
            {
                Cell cell = header.Cells.Add($"Header {c}");
                cell.BackgroundColor = Aspose.Pdf.Color.LightBlue;
                // Header uses a solid black border
                cell.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);
                // Set font styling for header cells via DefaultCellTextState
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    FontStyle = FontStyles.Bold
                };
            }

            // Add data rows with custom borders based on row index
            for (int r = 1; r <= 10; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 1; c <= 5; c++)
                {
                    Cell cell = row.Cells.Add($"R{r}C{c}");

                    // Apply custom border style:
                    // Even rows -> blue thin border
                    // Odd rows  -> red thick border
                    if (r % 2 == 0)
                    {
                        cell.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Blue);
                    }
                    else
                    {
                        cell.Border = new BorderInfo(BorderSide.All, 2f, Aspose.Pdf.Color.Red);
                    }
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
