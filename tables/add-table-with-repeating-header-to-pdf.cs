using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Create a table that starts on a new page and repeats the first row on each page
                Table table = new Table
                {
                    IsInNewPage = true,
                    RepeatingRowsCount = 1,
                    // Define column widths (optional, here two equal columns)
                    ColumnWidths = "200 200"
                };

                // Header row – this row will be repeated because RepeatingRowsCount = 1
                Row header = table.Rows.Add();
                Cell headerCell1 = header.Cells.Add("Header 1");
                Cell headerCell2 = header.Cells.Add("Header 2");
                headerCell1.BackgroundColor = Color.LightGray;
                headerCell2.BackgroundColor = Color.LightGray;

                // Data row
                Row data = table.Rows.Add();
                data.Cells.Add(new TextFragment("Value A"));
                data.Cells.Add(new TextFragment("Value B"));

                // Add the table to the page's content
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}