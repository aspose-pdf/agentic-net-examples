using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, Table, Row, HorizontalAlignment)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "centered_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Document lifecycle must be wrapped in a using block (deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a new table instance
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();

            // Optional: set the table's position on the page
            table.Left = 50;   // distance from the left edge
            table.Top  = 700;  // distance from the bottom edge

            // Align the whole table to the center of the page
            table.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;

            // Add a simple row with two cells (demo content)
            Aspose.Pdf.Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Insert the table into the first page's paragraph collection
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF (Document.Save without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Centered table PDF saved to '{outputPath}'.");
    }
}