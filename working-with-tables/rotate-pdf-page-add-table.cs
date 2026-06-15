using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment
using Aspose.Pdf.Drawing; // for BorderInfo, Color

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Rotate the first page 90 degrees clockwise
            Page page = doc.Pages[1];
            page.Rotate = Rotation.on90;

            // Create a simple table
            Table table = new Table();

            // Position the table on the page (coordinates in points)
            table.Left = 100; // distance from the left edge
            table.Top = 100;  // distance from the bottom edge

            // Optional: set a visible border
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // Define column widths (comma‑separated string)
            table.ColumnWidths = "100 200"; // first column 100 pt, second column 200 pt

            // Add first row (header)
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 1") } });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 2") } });

            // Add second row (data)
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Value 1") } });
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Value 2") } });

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated page with table saved to '{outputPath}'.");
    }
}
