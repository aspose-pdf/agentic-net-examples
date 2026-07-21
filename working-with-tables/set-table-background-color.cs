using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists – create a minimal one if necessary
        if (!File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add();
                seed.Save(inputPath);
            }
        }

        // Load the PDF and add a table with a semi‑transparent background
        using (Document doc = new Document(inputPath))
        {
            Table table = new Table();

            // 50 % opaque blue (alpha = 128 out of 255)
            table.BackgroundColor = Color.FromArgb(128, 0, 0, 255);

            // Add a simple row/cell so the table is visible
            Row row = table.Rows.Add();
            row.Cells.Add("Sample cell");

            // Place the table on the first page
            doc.Pages[1].Paragraphs.Add(table);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with background color saved to '{outputPath}'.");
    }
}
