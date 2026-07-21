using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Rotate the first page 90 degrees clockwise using the correct enum value
            Page page = doc.Pages[1];
            page.Rotate = Rotation.on90;

            // Create a table
            Table table = new Table
            {
                // Position the table on the page (coordinates are in points)
                Left = 100,
                Top = 100,
                // Optional visual styling – use float literals for line widths
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (two columns, each 200 points wide)
            table.ColumnWidths = "200 200";

            // Add a header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Header 1");
            Cell headerCell2 = header.Cells.Add("Header 2");
            // Apply bold style to header cells
            headerCell1.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12
            };
            headerCell2.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12
            };

            // Add some data rows
            for (int i = 1; i <= 3; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} Col 1");
                row.Cells.Add($"Row {i} Col 2");
            }

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated page with table saved to '{outputPath}'.");
    }
}
