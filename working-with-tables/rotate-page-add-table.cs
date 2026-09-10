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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Rotate the first page 90 degrees clockwise using the correct enum value
            Page page = doc.Pages[1]; // 1‑based indexing
            page.Rotate = Rotation.on90;

            // Create a simple table
            Table table = new Table
            {
                // Position the table on the page (coordinates are in points)
                Left = 100,
                Top = 200,
                // Define column widths (two columns, each 150 points wide)
                ColumnWidths = "150 150"
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Data row
            Row data = table.Rows.Add();
            data.Cells.Add("Value 1");
            data.Cells.Add("Value 2");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
