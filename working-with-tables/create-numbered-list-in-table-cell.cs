using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input/Output paths (adjust as needed)
        const string outputPath = "NumberedListInCell.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table with one column and one row
            Table table = new Table
            {
                ColumnWidths = "200", // width of the single column
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black)
            };
            page.Paragraphs.Add(table);

            // **Fix**: Add a row before accessing it
            Row row = table.Rows.Add();

            // Add a single cell to the newly created row
            Cell cell = row.Cells.Add();

            // ------------------------------------------------------------
            // Build a numbered list inside the cell using Paragraphs.
            // Each paragraph starts with a number followed by a period.
            // ------------------------------------------------------------
            for (int i = 1; i <= 5; i++)
            {
                // Create a TextFragment for the list item
                TextFragment tf = new TextFragment($"{i}. List item number {i}")
                {
                    TextState =
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 12,
                        ForegroundColor = Aspose.Pdf.Color.Black
                    }
                };

                // Add the fragment as a paragraph to the cell
                cell.Paragraphs.Add(tf);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with numbered list saved to '{outputPath}'.");
    }
}
