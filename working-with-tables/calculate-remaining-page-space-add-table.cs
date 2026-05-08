using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextFragment

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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Calculate the bounding box of existing content (without visible margins)
            Aspose.Pdf.Rectangle contentBox = page.CalculateContentBBox();

            // Height of the existing content
            double contentHeight = contentBox.URY - contentBox.LLY;

            // Total usable page height (excluding page margins)
            double pageHeight = page.PageInfo.PureHeight;

            // Remaining vertical space on the page
            double remainingSpace = pageHeight - contentHeight;

            // Create a simple table
            Table table = new Table
            {
                ColumnWidths = "100 100"
            };

            // First row
            Row row1 = new Row();
            row1.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell 1") } });
            row1.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell 2") } });
            table.Rows.Add(row1);

            // Second row
            Row row2 = new Row();
            row2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell 3") } });
            row2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell 4") } });
            table.Rows.Add(row2);

            // Get the height the table would occupy on this page
            double tableHeight = table.GetHeight(page);

            // Ensure the table fits into the remaining space
            if (tableHeight > remainingSpace)
            {
                Console.WriteLine("Not enough space on the page for the table. Adjusting position to start at the top margin.");
                // Position the table at the top of the page (below the top margin)
                table.Top = (float)page.PageInfo.Margin.Top;
            }
            else
            {
                // Position the table just below the existing content
                table.Top = (float)(contentBox.LLY - tableHeight);
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
