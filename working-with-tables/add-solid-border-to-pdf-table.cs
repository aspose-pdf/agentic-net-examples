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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a simple table with two columns and apply a solid border to the whole table
            Table table = new Table
            {
                // Define column widths (percentage or absolute units)
                ColumnWidths = "200 200",

                // Apply a solid black border of 1 point thickness to the entire table
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                // Optionally set the default cell border as well
                DefaultCellBorder = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell B1");

            // Insert the table into the first page's paragraph collection
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with bordered table saved to '{outputPath}'.");
    }
}
