using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Determine the usable page height.
            // ------------------------------------------------------------
            // PureHeight = page height without the page margins.
            double pageHeight = page.PageInfo.PureHeight;

            // Calculate the bounding box of the existing content.
            // The rectangle returned uses PDF coordinates (LLX, LLY, URX, URY).
            Aspose.Pdf.Rectangle contentBox = page.CalculateContentBBox();

            // Height of the existing content.
            double contentHeight = contentBox.URY - contentBox.LLY;

            // Define the top and bottom margins you want to keep (in points).
            // Use float literals because Table.Top/Left expect float values.
            const float topMargin    = 36f; // 0.5 inch
            const float bottomMargin = 36f; // 0.5 inch

            // Remaining vertical space where a new table can be placed.
            double remainingHeight = pageHeight - contentHeight - topMargin - bottomMargin;

            Console.WriteLine($"Page pure height   : {pageHeight}");
            Console.WriteLine($"Existing content   : {contentHeight}");
            Console.WriteLine($"Remaining height   : {remainingHeight}");

            // ------------------------------------------------------------
            // 2. Create a table that fits into the remaining space.
            // ------------------------------------------------------------
            Table table = new Table
            {
                // Position the table just above the bottom margin.
                // Top coordinate is measured from the bottom of the page.
                Top = bottomMargin, // float matches property type
                // Left coordinate can be set as needed; here we use a left margin.
                Left = 50f,
                // Set a width that fits the page (page width minus side margins).
                // For simplicity we use a fixed width.
                ColumnWidths = "500"
            };

            // Add a few rows as an example.
            for (int i = 0; i < 5; i++)
            {
                Row row = table.Rows.Add();
                Cell cell = row.Cells.Add();
                cell.Paragraphs.Add(new TextFragment($"Row {i + 1}"));
            }

            // Optionally, check the table height before adding it.
            double tableHeight = table.GetHeight(page);
            Console.WriteLine($"Calculated table height: {tableHeight}");

            // ------------------------------------------------------------
            // 3. Add the table to the page.
            // ------------------------------------------------------------
            page.Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
