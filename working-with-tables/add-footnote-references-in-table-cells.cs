using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_footnotes.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // Create a simple table with footnote references
            // -------------------------------------------------
            Page page = doc.Pages[1];

            // Create a table and add it to the page
            Table table = new Table();
            table.ColumnWidths = "100 300"; // Item column, Description column
            table.Border = new BorderInfo(BorderSide.All, 0.5f);
            page.Paragraphs.Add(table);

            // Header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Item");
            Cell headerCell2 = header.Cells.Add("Description");
            headerCell1.BackgroundColor = Color.LightGray;
            headerCell2.BackgroundColor = Color.LightGray;

            // Data row
            Row dataRow = table.Rows.Add();
            // First cell – plain text
            Cell itemCell = dataRow.Cells.Add("Widget A");

            // Second cell – description with superscript footnote number
            Cell descCell = dataRow.Cells.Add();
            // Main description text
            TextFragment desc = new TextFragment("This widget is used for testing ");
            // Superscript footnote number (smaller font)
            TextFragment footRef = new TextFragment("1");
            footRef.TextState.FontSize = 8;                     // smaller size
            footRef.TextState.Font = FontRepository.FindFont("Helvetica");
            // Add both fragments to the cell's paragraph collection
            descCell.Paragraphs.Add(desc);
            descCell.Paragraphs.Add(footRef);

            // -------------------------------------------------
            // Create the actual footnote content at the end of the document
            // -------------------------------------------------
            // Add a blank line to act as a top margin before the footnote
            page.Paragraphs.Add(new TextFragment("\n"));

            TextFragment footnote = new TextFragment("1. This is the footnote explaining Widget A.");
            footnote.TextState.Font = FontRepository.FindFont("Helvetica");
            footnote.TextState.FontSize = 9;
            page.Paragraphs.Add(footnote);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with footnotes saved to '{outputPath}'.");
    }
}
