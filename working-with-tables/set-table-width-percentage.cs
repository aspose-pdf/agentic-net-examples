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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a table whose total width occupies 80% of the page width.
            // This is achieved by assigning percentage values to each column.
            // Two columns each get 40% → 40% + 40% = 80% of the page width.
            Table table = new Table
            {
                ColumnWidths = "40% 40%",
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a row with two cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Insert the table into the first page's paragraph collection
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with 80% width saved to '{outputPath}'.");
    }
}
