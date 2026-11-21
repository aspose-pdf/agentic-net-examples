using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a table and configure its default cell border to be solid (dashed style is not supported directly on Table borders)
            Table table = new Table();

            // Set the default border for all cells using the BorderInfo constructor (no DashArray property exists on BorderInfo)
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 1f, Color.Black);

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");

            // Insert the table into the first page's paragraph collection
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with borders saved to '{outputPath}'.");
    }
}
