using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, rotate the first page, add a table, and save.
        using (Document doc = new Document(inputPath))
        {
            // Rotate the first page 90 degrees clockwise.
            Page page = doc.Pages[1]; // 1‑based indexing
            page.Rotate = Rotation.on90; // Correct enum value

            // Create a simple table.
            Table table = new Table
            {
                // Position the table on the page (coordinates are in points).
                // After rotation, the coordinates are interpreted relative to the rotated page.
                Left = 50,
                Top = 100,
                // Optional visual styling.
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black)
            };

            // Define two columns with equal width using the string‑based ColumnWidths property.
            table.ColumnWidths = "200 200";

            // Add header row.
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add();
            headerCell1.DefaultCellTextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica-Bold") };
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell headerCell2 = header.Cells.Add();
            headerCell2.DefaultCellTextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica-Bold") };
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));

            // Add a data row.
            Row data = table.Rows.Add();
            Cell dataCell1 = data.Cells.Add();
            dataCell1.Paragraphs.Add(new TextFragment("Cell A"));
            Cell dataCell2 = data.Cells.Add();
            dataCell2.Paragraphs.Add(new TextFragment("Cell B"));

            // Insert the table into the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
