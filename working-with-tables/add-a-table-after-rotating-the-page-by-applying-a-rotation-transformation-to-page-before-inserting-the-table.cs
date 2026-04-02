using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Rotate the page 90 degrees clockwise (use the correct enum value)
            page.Rotate = Rotation.on90;

            // Create a new table
            Table table = new Table
            {
                // Position the table on the rotated page
                // Left and Top are measured in points (1/72 inch)
                Left = 100,   // distance from the left edge of the page
                Top  = 200,   // distance from the top edge of the page
                // Optional visual styling (use float literals for width)
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (optional)
            table.ColumnWidths = "150 150";

            // Add a header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell headerCell2 = header.Cells.Add();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));

            // Add a data row
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = dataRow.Cells.Add();
            dataCell1.Paragraphs.Add(new TextFragment("Value A"));
            Cell dataCell2 = dataRow.Cells.Add();
            dataCell2.Paragraphs.Add(new TextFragment("Value B"));

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
