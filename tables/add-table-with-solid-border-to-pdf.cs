using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

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
            // Create a table that will be placed on the first page
            Table table = new Table();

            // Assign a solid border (1 point width) to the whole table
            table.Border = new BorderInfo(BorderSide.All, 1f);

            // Define two columns with equal width (optional, but clarifies layout)
            table.ColumnWidths = "200 200";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell B1");

            // Insert the table into the first page's paragraph collection
            // Pages are 1‑based in Aspose.Pdf
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF; the using block ensures the document stays alive until Save completes
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with solid border added and saved to '{outputPath}'.");
    }
}