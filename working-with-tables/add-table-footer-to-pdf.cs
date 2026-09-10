using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
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
            // 1. Create a visual Table and add it to the first page
            // -------------------------------------------------
            Table visualTable = new Table
            {
                ColumnWidths = "100 200", // two columns
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Header row (visual)
            Row headerRow = new Row();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            visualTable.Rows.Add(headerRow);

            // Body row (visual)
            Row bodyRow = new Row();
            bodyRow.Cells.Add("Data 1");
            bodyRow.Cells.Add("Data 2");
            visualTable.Rows.Add(bodyRow);

            // Footer row (visual) – this will appear at the bottom of each page when the table splits
            Row footerRow = new Row();
            footerRow.Cells.Add("Footer left");
            footerRow.Cells.Add("Footer right");
            visualTable.Rows.Add(footerRow);

            // Add the table to the first page
            doc.Pages[1].Paragraphs.Add(visualTable);

            // -------------------------------------------------
            // 2. Create logical structure for the table and its footer
            // -------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the tagged content
            StructureElement root = tagged.RootElement;

            // Table element in the logical structure
            TableElement tableStruct = tagged.CreateTableElement();
            root.AppendChild(tableStruct); // attach table to the root

            // Create the TFoot (table footer) element
            TableTFootElement tFoot = tagged.CreateTableTFootElement();
            tableStruct.AppendChild(tFoot); // attach footer to the table

            // Create a row inside the TFoot
            TableTRElement footRowStruct = tFoot.CreateTR();

            // Create cells for the footer row
            TableTDElement footCell1 = tagged.CreateTableTDElement();
            footCell1.SetText("Footer left");
            footRowStruct.AppendChild(footCell1);

            TableTDElement footCell2 = tagged.CreateTableTDElement();
            footCell2.SetText("Footer right");
            footRowStruct.AppendChild(footCell2);

            // Optional: set alternative text for accessibility
            tFoot.AlternativeText = "Table footer repeated on each page";

            // -------------------------------------------------
            // 3. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table footer: {outputPath}");
    }
}