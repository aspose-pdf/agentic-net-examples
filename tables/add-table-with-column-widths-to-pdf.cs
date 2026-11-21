using System;
using System.IO;
using Aspose.Pdf;

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
            // Create a new Table object
            Table table = new Table
            {
                // Set column widths: first column 80 points, second column 120 points
                ColumnWidths = "80 120",
                // Adjust columns to fit the window width automatically
                ColumnAdjustment = Aspose.Pdf.ColumnAdjustment.AutoFitToWindow
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Value A");
            data.Cells.Add("Value B");

            // Insert the table into the first page's paragraph collection
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}
