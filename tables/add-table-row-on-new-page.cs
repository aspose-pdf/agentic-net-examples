using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a visual table and add it to the page's paragraph collection
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Define column widths (optional, here two equal columns)
            table.ColumnWidths = "200 200";

            // Add first row
            Row row1 = table.Rows.Add();
            row1.Cells.Add(new TextFragment("Header 1"));
            row1.Cells.Add(new TextFragment("Header 2"));

            // Add second row
            Row row2 = table.Rows.Add();
            row2.Cells.Add(new TextFragment("Data A"));
            row2.Cells.Add(new TextFragment("Data B"));

            // Add third (last) row and force it onto a new page
            Row row3 = table.Rows.Add();
            row3.Cells.Add(new TextFragment("Footer 1"));
            row3.Cells.Add(new TextFragment("Footer 2"));
            row3.IsInNewPage = true; // start this row on a new page

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}