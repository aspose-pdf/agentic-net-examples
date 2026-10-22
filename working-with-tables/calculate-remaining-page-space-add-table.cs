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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Page dimensions
            double pageHeight = page.PageInfo.Height;
            double pageWidth  = page.PageInfo.Width;

            // Margins – if not set they default to 0
            double marginTop    = page.PageInfo.Margin?.Top ?? 0;
            double marginBottom = page.PageInfo.Margin?.Bottom ?? 0;
            double marginLeft   = page.PageInfo.Margin?.Left ?? 0;
            double marginRight  = page.PageInfo.Margin?.Right ?? 0;

            // Approximate height of existing content (e.g., text fragments)
            double existingContentHeight = 0;
            foreach (TextFragment fragment in page.Paragraphs.OfType<TextFragment>())
            {
                // Use the font size as a rough line height; multiply by a line‑spacing factor
                if (fragment.TextState != null && fragment.TextState.FontSize > 0)
                {
                    existingContentHeight += fragment.TextState.FontSize * 1.2;
                }
            }

            // Remaining vertical space on the page
            double remainingSpace = pageHeight - (marginTop + marginBottom + existingContentHeight);
            Console.WriteLine($"Page height: {pageHeight}");
            Console.WriteLine($"Margins (top/bottom): {marginTop}/{marginBottom}");
            Console.WriteLine($"Existing content height: {existingContentHeight}");
            Console.WriteLine($"Remaining space: {remainingSpace}");

            // Create a table that fits into the remaining space
            Table table = new Table
            {
                // Position the table just below the existing content using MarginInfo
                Margin = new MarginInfo(marginLeft, marginRight, marginTop + existingContentHeight, marginBottom),
                // Define two equal‑width columns (values are percentages of the page width)
                ColumnWidths = "100 100",
                // Optional cell padding
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.BackgroundColor = Aspose.Pdf.Color.LightGray; // fully qualified to avoid ambiguity

            // Approximate row height; calculate how many rows fit
            double rowHeight = 20; // rough estimate
            int maxRows = (int)Math.Floor(remainingSpace / rowHeight);

            for (int i = 0; i < maxRows; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Item {i + 1}");
                row.Cells.Add($"Value {i + 1}");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
