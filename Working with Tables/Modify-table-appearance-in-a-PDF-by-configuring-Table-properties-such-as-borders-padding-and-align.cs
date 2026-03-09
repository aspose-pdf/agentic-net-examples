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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table();

            // Position the table on the page
            table.Left = 50;   // X coordinate
            table.Top = 700;   // Y coordinate

            // Set table alignment (centered)
            table.Alignment = HorizontalAlignment.Center;

            // Configure the outer border of the table using BorderInfo
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // Set default cell padding (5 points on all sides) using MarginInfo
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5); // left, right, top, bottom

            // Configure default cell border using BorderInfo
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);

            // ----- Add header row -----
            Row headerRow = new Row();
            table.Rows.Add(headerRow);

            Cell headerCell1 = new Cell();
            headerCell1.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            headerRow.Cells.Add(headerCell1);

            Cell headerCell2 = new Cell();
            headerCell2.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));
            headerRow.Cells.Add(headerCell2);

            // ----- Add data row -----
            Row dataRow = new Row();
            table.Rows.Add(dataRow);

            Cell dataCell1 = new Cell();
            dataCell1.Paragraphs.Add(new TextFragment("Data 1"));
            dataRow.Cells.Add(dataCell1);

            Cell dataCell2 = new Cell();
            dataCell2.Paragraphs.Add(new TextFragment("Data 2"));
            dataRow.Cells.Add(dataCell2);

            // Add the table to the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with customized appearance saved to '{outputPath}'.");
    }
}
