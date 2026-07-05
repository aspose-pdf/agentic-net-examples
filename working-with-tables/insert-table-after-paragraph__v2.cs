using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the existing PDF (document disposal handled by using)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // 1. Create a text fragment that will precede the table
            // -------------------------------------------------
            TextFragment txtFragment = new TextFragment("Below is a sample table:")
            {
                // Position the fragment (X, Y) – coordinates are measured from the bottom‑left corner
                Position = new Position(50, 700)
            };
            // Optional: set formatting (font, size, etc.)
            txtFragment.TextState.FontSize = 12;
            txtFragment.TextState.Font = FontRepository.FindFont("Helvetica");

            // Add the fragment to the page
            page.Paragraphs.Add(txtFragment);

            // Store the index of the fragment we just added (Count‑1 after the Add)
            int txtIndex = page.Paragraphs.Count - 1;

            // -------------------------------------------------
            // 2. Create a table (Table derives from BaseParagraph)
            // -------------------------------------------------
            Table table = new Table
            {
                // Define column widths (three columns in this example)
                ColumnWidths = "100 200 100"
            };

            // First row – header cells
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // Second row – data cells
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");
            dataRow.Cells.Add("Cell 3");

            // -------------------------------------------------
            // 3. Insert the table after the previously added fragment
            // -------------------------------------------------
            page.Paragraphs.Insert(txtIndex + 1, table);

            // -------------------------------------------------
            // 4. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
