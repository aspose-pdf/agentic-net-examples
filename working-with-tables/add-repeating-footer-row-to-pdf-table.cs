using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Create a visual table and add it to the first page
            // -------------------------------------------------
            Page page = doc.Pages[1];

            Table table = new Table
            {
                // Ensure the last row repeats on each page as a footer.
                // Aspose.Pdf versions prior to 23.10 do not expose the RepeatingRowsStyle enum.
                // In such cases, setting RepeatingRowsCount to 1 makes the last row repeat.
                // The visual appearance (header vs. footer) is handled by the logical structure below.
                RepeatingRowsCount = 1,
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Header row (optional)
            Row header = new Row();
            header.Cells.Add("Column A");
            header.Cells.Add("Column B");
            table.Rows.Add(header);

            // Body rows
            for (int i = 1; i <= 20; i++)
            {
                Row body = new Row();
                body.Cells.Add($"Item {i}");
                body.Cells.Add($"Value {i * 10}");
                table.Rows.Add(body);
            }

            // Footer row (visual) – this will repeat on each page because RepeatingRowsCount = 1
            Row footer = new Row();
            footer.Cells.Add("Total");
            footer.Cells.Add("2000");
            table.Rows.Add(footer);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // -------------------------------------------------
            // 2. Create logical structure for the table with a TFoot element
            // -------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the tagged content tree
            StructureElement root = tagged.RootElement;

            // Create the table structure element
            TableElement tableStruct = tagged.CreateTableElement();
            tableStruct.AlternativeText = "Sample data table with footer";
            root.AppendChild(tableStruct);

            // Create TFoot (table footer) logical element
            TableTFootElement tFoot = tagged.CreateTableTFootElement();
            tableStruct.AppendChild(tFoot);

            // Create a table row inside the TFoot
            TableTRElement footRow = tFoot.CreateTR();

            // First cell of the footer row
            TableTDElement footCell1 = tagged.CreateTableTDElement();
            footCell1.SetText("Total");
            footRow.AppendChild(footCell1);

            // Second cell of the footer row
            TableTDElement footCell2 = tagged.CreateTableTDElement();
            footCell2.SetText("2000");
            footRow.AppendChild(footCell2);

            // Append the footer row to the TFoot element
            tFoot.AppendChild(footRow);

            // -------------------------------------------------
            // 3. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with footer row saved to '{outputPath}'.");
    }
}
