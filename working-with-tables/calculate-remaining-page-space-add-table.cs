using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Table is in Aspose.Pdf namespace

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

        // Open the PDF – Document must be disposed via using
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Calculate the bounding box of existing content (without visible margins)
            // Returns an Aspose.Pdf.Rectangle
            Aspose.Pdf.Rectangle contentRect = page.CalculateContentBBox();

            // Height of the existing content
            double contentHeight = contentRect.URY - contentRect.LLY;

            // Pure page height excludes page margins (see PageInfo.PureHeight)
            double purePageHeight = page.PageInfo.PureHeight;

            // Remaining vertical space on the page after the existing content
            double remainingSpace = purePageHeight - contentHeight;

            Console.WriteLine($"Page pure height: {purePageHeight}");
            Console.WriteLine($"Existing content height: {contentHeight}");
            Console.WriteLine($"Remaining space: {remainingSpace}");

            // Create a simple table
            Table table = new Table();

            // Example: two columns, three rows
            table.ColumnWidths = "200 200"; // two columns, each 200 units wide
            table.Rows.Add(new Row()); // header row
            table.Rows[1].Cells.Add("Header 1");
            table.Rows[1].Cells.Add("Header 2");

            table.Rows.Add(new Row()); // first data row
            table.Rows[2].Cells.Add("Cell 1");
            table.Rows[2].Cells.Add("Cell 2");

            table.Rows.Add(new Row()); // second data row
            table.Rows[3].Cells.Add("Cell 3");
            table.Rows[3].Cells.Add("Cell 4");

            // Determine the height of the table (optional, for validation)
            double tableHeight = table.GetHeight(page);
            Console.WriteLine($"Table height: {tableHeight}");

            // If the table fits into the remaining space, position it just below existing content
            if (tableHeight <= remainingSpace)
            {
                // Table.Top and Table.Left expect float values – cast from double
                table.Top = (float)contentRect.URY; // upper Y coordinate where the table starts
                table.Left = (float)contentRect.LLX; // align with left edge of existing content
            }
            else
            {
                // Table does not fit; for this example we start it on a new page
                Page newPage = doc.Pages.Add();
                newPage.Paragraphs.Add(table);
                doc.Save(outputPath);
                Console.WriteLine("Table did not fit on the first page; added to a new page.");
                return;
            }

            // Add the table to the original page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
    }
}
