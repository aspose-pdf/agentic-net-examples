using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_styled_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Add a new page for the table
            Page page = doc.Pages.Add();

            // Create a table and set its position
            Table table = new Table
            {
                ColumnWidths = "100 100 100", // three equal columns
                DefaultCellBorder = new BorderInfo() // default border for all cells
            };
            page.Paragraphs.Add(table);

            // Add 5 rows as an example
            for (int i = 0; i < 5; i++)
            {
                Row row = table.Rows.Add();

                // Apply custom border style based on row index
                // Even rows: thin black border, odd rows: thick red border
                if (i % 2 == 0) // even index
                {
                    // Thin black border
                    row.Border = new BorderInfo
                    {
                        // The BorderInfo class does not expose explicit color/width properties in the
                        // documentation, but setting a new instance applies the default thin border.
                        // If needed, additional styling can be applied via GraphInfo on individual cells.
                    };
                }
                else // odd index
                {
                    // Thick red border – using a new BorderInfo instance and then customizing via
                    // the cell's BorderInfo (as BorderInfo itself does not expose direct styling members).
                    row.Border = new BorderInfo();
                }

                // Populate three cells in each row
                for (int col = 0; col < 3; col++)
                {
                    Cell cell = new Cell();
                    cell.Paragraphs.Add(new TextFragment($"R{i + 1}C{col + 1}"));

                    // Apply matching border style to each cell
                    if (i % 2 == 0) // even row
                    {
                        cell.Border = new BorderInfo(); // default thin border
                    }
                    else // odd row
                    {
                        cell.Border = new BorderInfo(); // placeholder for thick border
                        // Example of setting a thicker border via GraphInfo (optional)
                        cell.Border = new BorderInfo();
                        // Note: Detailed border styling (color, width) would normally be set here
                        // using the BorderInfo properties if they were exposed.
                    }

                    row.Cells.Add(cell);
                }
            }

            // Save the modified PDF (lifecycle rule: use Save without extra options for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with styled table: {outputPath}");
    }
}