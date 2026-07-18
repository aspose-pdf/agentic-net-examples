using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a simple table with 2 columns and 2 rows
            Table table = new Table();

            // Configure the table border thickness to 2 points using the BorderInfo constructor
            table.Border = new BorderInfo(BorderSide.All, 2f, Aspose.Pdf.Color.Black);

            // Set column widths (optional)
            table.ColumnWidths = "200 200";

            // First row
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Header 1");
            row1.Cells.Add("Header 2");

            // Second row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell A");
            row2.Cells.Add("Cell B");

            // Add the table to the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with 2‑point border saved to '{outputPath}'.");
    }
}
