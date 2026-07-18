using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_table.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Build a simple table
            Table table = new Table
            {
                // Define three equal columns
                ColumnWidths = "100 100 100",
                // Set a thin black border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                // Add some padding inside cells
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Sample data rows
            for (int i = 1; i <= 3; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"R{i}C1");
                row.Cells.Add($"R{i}C2");
                row.Cells.Add($"R{i}C3");
            }

            // Position the table on the page before rotation
            table.Left = 100;   // X coordinate
            table.Top  = 400;   // Y coordinate

            // Save current graphics state
            page.Contents.Add(new GSave());

            // Apply a 90‑degree rotation matrix (π/2 radians)
            Matrix rotation = Matrix.Rotation(Math.PI / 2);
            // Use the correct operator class name: ConcatenateMatrix
            page.Contents.Add(new ConcatenateMatrix(rotation));

            // Add the table – it will be rendered using the rotation matrix
            page.Paragraphs.Add(table);

            // Restore graphics state so later content is not rotated
            page.Contents.Add(new GRestore());

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
