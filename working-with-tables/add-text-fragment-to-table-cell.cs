using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF (can be empty)
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF (or create a new one if needed)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to host the table
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table with one row and one cell
            Table table = new Table
            {
                ColumnWidths = "200",   // width of the single column
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();

            // Create a TextFragment with specific font and size
            TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");
            tf.TextState.Font = FontRepository.FindFont("Arial"); // specify font
            tf.TextState.FontSize = 14;                           // specify size

            // Add the TextFragment to the cell's paragraph collection
            cell.Paragraphs.Add(tf);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}