using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Needed for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table();

            // Set the left offset by configuring the MarginInfo.Left property (value in points)
            table.Margin = new MarginInfo();
            table.Margin.Left = 50; // offset 50 points from the left margin

            // Optional: add a simple row and cell for demonstration
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextFragment("Sample cell"));

            // Add the table to the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table offset applied and saved to '{outputPath}'.");
    }
}
