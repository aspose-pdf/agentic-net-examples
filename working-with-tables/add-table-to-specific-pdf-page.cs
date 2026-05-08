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
        const int targetPageNumber = 2; // 1‑based page number where the table will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate the requested page number (Aspose.Pdf pages are 1‑based)
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range (1‑{doc.Pages.Count}).");
                return;
            }

            // Create a simple 2×2 table
            Table table = new Table
            {
                Border = new BorderInfo(BorderSide.All, 1.0f, Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // First row
            Row row1 = table.Rows.Add();
            Cell cell11 = row1.Cells.Add();
            cell11.Paragraphs.Add(new TextFragment("R1C1"));
            Cell cell12 = row1.Cells.Add();
            cell12.Paragraphs.Add(new TextFragment("R1C2"));

            // Second row
            Row row2 = table.Rows.Add();
            Cell cell21 = row2.Cells.Add();
            cell21.Paragraphs.Add(new TextFragment("R2C1"));
            Cell cell22 = row2.Cells.Add();
            cell22.Paragraphs.Add(new TextFragment("R2C2"));

            // Insert the table into the specified page
            Page page = doc.Pages[targetPageNumber]; // Pages collection is 1‑based
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added to page {targetPageNumber} and saved as '{outputPath}'.");
    }
}
