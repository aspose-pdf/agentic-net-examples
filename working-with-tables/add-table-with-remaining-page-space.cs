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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Pure page height excludes page margins
            double pagePureHeight = page.PageInfo.PureHeight;

            // Bounding box of existing content (without visible margins)
            Aspose.Pdf.Rectangle contentBBox = page.CalculateContentBBox();

            double existingContentHeight = contentBBox.URY - contentBBox.LLY;

            // Remaining vertical space on the page
            double remainingHeight = pagePureHeight - existingContentHeight;

            Console.WriteLine($"Page pure height: {pagePureHeight}");
            Console.WriteLine($"Existing content height: {existingContentHeight}");
            Console.WriteLine($"Remaining height for table: {remainingHeight}");

            // Create a simple table
            Table table = new Table();

            // Position the table just below the existing content (cast to float because Table.Left/Top are float)
            table.Left = (float)contentBBox.LLX;
            table.Top = (float)contentBBox.URY; // start after current content

            // Define column widths (example: two equal columns)
            table.ColumnWidths = "100 100";

            // Add a single row with two cells
            Row row = new Row();
            Cell cell1 = new Cell();
            cell1.Paragraphs.Add(new TextFragment("Cell 1"));
            Cell cell2 = new Cell();
            cell2.Paragraphs.Add(new TextFragment("Cell 2"));
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            table.Rows.Add(row);

            // Calculate the table height on this page
            double tableHeight = table.GetHeight(page);
            Console.WriteLine($"Calculated table height: {tableHeight}");

            // Add the table only if it fits into the remaining space
            if (tableHeight <= remainingHeight)
            {
                page.Paragraphs.Add(table);
                Console.WriteLine("Table added to the page.");
            }
            else
            {
                Console.WriteLine("Table does not fit in the remaining space.");
            }

            // Save the modified document (lifecycle rule: use using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
