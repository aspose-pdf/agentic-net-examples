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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Create a table and set its Z‑index higher than default (0) 
            // so it appears above other page elements.
            Table table = new Table
            {
                ZIndex = 10,                     // Controls overlapping order
                ColumnWidths = "150 150",        // Define column widths
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add data row
            Row data = table.Rows.Add();
            data.Cells.Add("Value 1");
            data.Cells.Add("Value 2");

            // Place the table on the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with ZIndex saved to '{outputPath}'.");
    }
}