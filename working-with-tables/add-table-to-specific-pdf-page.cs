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
        const int targetPageNumber = 2; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {targetPageNumber}. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Access the required page (1‑based indexing)
            Page page = doc.Pages[targetPageNumber];

            // Create a simple table with 2 columns and 2 rows (header + data)
            Table table = new Table
            {
                Border = new BorderInfo(BorderSide.All, 0.5f), // thin border around cells
                ColumnWidths = "50% 50%" // equal column widths
            };

            // Header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Name");
            Cell headerCell2 = header.Cells.Add("Value");

            // Make header cells bold
            headerCell1.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };
            headerCell2.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };

            // Optional: background color for header cells
            headerCell1.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell2.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Data row
            Row data = table.Rows.Add();
            data.Cells.Add("Item A");
            data.Cells.Add("42");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added to page {targetPageNumber} and saved as '{outputPath}'.");
    }
}