using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (or add a new one if the document is empty)
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table
            Table table = new Table();
            // Set a semi‑transparent light gray background (50 % opacity)
            // Alpha value 128 (0‑255) gives 50 % opacity
            table.BackgroundColor = Color.FromArgb(128, 230, 230, 230);

            // Define a simple 2‑column layout (widths in points)
            table.ColumnWidths = "200 200"; // two columns, each 200 points wide

            // Add a header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Color.FromRgb(180, 180, 180); // darker gray for header

            // Header cell 1
            Cell headerCell1 = new Cell();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            header.Cells.Add(headerCell1);

            // Header cell 2
            Cell headerCell2 = new Cell();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));
            header.Cells.Add(headerCell2);

            // Add a data row
            Row data = table.Rows.Add();

            Cell dataCell1 = new Cell();
            dataCell1.Paragraphs.Add(new TextFragment("Cell A"));
            data.Cells.Add(dataCell1);

            Cell dataCell2 = new Cell();
            dataCell2.Paragraphs.Add(new TextFragment("Cell B"));
            data.Cells.Add(dataCell2);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table background color to '{outputPath}'.");
    }
}
