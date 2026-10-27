using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Rotate the first page 90 degrees clockwise using the correct Rotate property
            Page page = doc.Pages[1];
            page.Rotate = Rotation.on90;

            // Access tagged content for accessibility
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a tagged table element (accessibility) and attach it to the root
            TableElement tableTag = tagged.CreateTableElement();
            tableTag.AlternativeText = "Rotated page data table";
            root.AppendChild(tableTag);

            // Build table header (tagged)
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            tableTag.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column 1");
            headerRow.AppendChild(th1);
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column 2");
            headerRow.AppendChild(th2);

            // Build table body (tagged)
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            tableTag.AppendChild(tbody);
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Value A");
            dataRow.AppendChild(td1);
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Value B");
            dataRow.AppendChild(td2);

            // ------------------------------------------------------------
            // Visual representation: create a regular Aspose.Pdf.Table and add it to the page
            // ------------------------------------------------------------
            Table visualTable = new Table();
            // Define column widths (optional)
            visualTable.ColumnWidths = "200 200";

            // Header row
            Row visualHeader = visualTable.Rows.Add();
            Cell headerCell1 = visualHeader.Cells.Add("Column 1");
            Cell headerCell2 = visualHeader.Cells.Add("Column 2");
            // Apply simple formatting to header cells
            headerCell1.BackgroundColor = Color.LightGray;
            headerCell2.BackgroundColor = Color.LightGray;

            // Data row
            Row visualData = visualTable.Rows.Add();
            visualData.Cells.Add("Value A");
            visualData.Cells.Add("Value B");

            // Add the visual table to the page's paragraphs collection
            page.Paragraphs.Add(visualTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
