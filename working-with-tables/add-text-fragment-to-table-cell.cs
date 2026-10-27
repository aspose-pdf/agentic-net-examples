using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_text.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page's paragraphs collection
            Table table = new Table
            {
                // Optional: set table borders for visibility
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };
            page.Paragraphs.Add(table);

            // Add a single row to the table
            Row row = table.Rows.Add();

            // Add a single cell to the row
            Cell cell = row.Cells.Add();

            // Create a TextFragment with the desired text
            TextFragment textFragment = new TextFragment("Hello, Aspose.Pdf!");

            // Set the font and size using the TextState property
            // FontRepository.FindFont searches for a system font; fallback to Helvetica if not found
            textFragment.TextState.Font = FontRepository.FindFont("Arial") ?? FontRepository.FindFont("Helvetica");
            textFragment.TextState.FontSize = 14; // Desired font size

            // Add the TextFragment to the cell's paragraphs collection
            cell.Paragraphs.Add(textFragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}