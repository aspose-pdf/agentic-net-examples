using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for BorderInfo and BorderSide

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

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table();

            // Set a default border for all cells (all sides, 0.5 point width)
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);

            // Add rows and cells
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 3");
            row2.Cells.Add("Cell 4");

            // Ensure the document has at least four pages
            while (doc.Pages.Count < 4)
                doc.Pages.Add();

            // Replace the content on page 4 with the new table
            Page pageFour = doc.Pages[4];
            pageFour.Paragraphs.Clear();      // remove existing paragraphs (including any previous table)
            pageFour.Paragraphs.Add(table);   // insert the newly created table

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}