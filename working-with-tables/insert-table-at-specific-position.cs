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

        // Load the existing PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Aspose.Pdf.Page page = doc.Pages[1];

            // Create a new table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();

            // Desired absolute position on the page (X, Y)
            float x = 100f; // horizontal coordinate
            float y = 500f; // vertical coordinate

            // Table does not expose a Position property directly; use Left and Top for absolute placement
            table.Left = x;
            table.Top  = y;

            // Define column widths (example: three equal columns)
            table.ColumnWidths = "100 100 100";

            // Add a header row
            Aspose.Pdf.Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // Add a data row
            Aspose.Pdf.Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");
            dataRow.Cells.Add("Cell 3");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted at ({100}, {500}) and saved to '{outputPath}'.");
    }
}