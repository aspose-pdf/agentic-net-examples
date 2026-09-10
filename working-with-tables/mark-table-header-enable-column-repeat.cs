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
        const string outputPath = "output.pdf";

        // Load existing PDF or create a new one if the file does not exist
        Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document();
        if (doc.Pages.Count == 0) doc.Pages.Add();

        // Use the first page for the table
        Page page = doc.Pages[1];

        // -------------------------------------------------
        // Visual table (appears in the PDF)
        // -------------------------------------------------
        Table table = new Table
        {
            ColumnWidths = "100 200",          // two columns
            RepeatingColumnsCount = 1         // repeat first column on each new page
        };

        // Header row (visual)
        Row visualHeader = table.Rows.Add();
        visualHeader.Cells.Add("Header 1");
        visualHeader.Cells.Add("Header 2");

        // Sample data rows
        for (int i = 1; i <= 20; i++)
        {
            Row r = table.Rows.Add();
            r.Cells.Add($"Item {i}");
            r.Cells.Add($"Value {i}");
        }

        // Add the visual table to the page
        page.Paragraphs.Add(table);

        // -------------------------------------------------
        // Tagged structure (accessibility)
        // -------------------------------------------------
        ITaggedContent tagged = doc.TaggedContent;
        StructureElement root = tagged.RootElement;

        // Create a table structure element and enable column repeat
        TableElement tableElem = tagged.CreateTableElement();
        tableElem.RepeatingColumnsCount = 1; // repeat first column on each new page
        root.AppendChild(tableElem);

        // Create the table header (THead) – this will be repeated on each page fragment
        TableTHeadElement thead = tagged.CreateTableTHeadElement();
        tableElem.AppendChild(thead);
        TableTRElement headerRow = tagged.CreateTableTRElement();
        thead.AppendChild(headerRow);

        // Mark the first column as a header cell (TH)
        TableTHElement th1 = tagged.CreateTableTHElement();
        th1.SetText("Header 1");
        headerRow.AppendChild(th1);

        // Second column header (also a TH for consistency)
        TableTHElement th2 = tagged.CreateTableTHElement();
        th2.SetText("Header 2");
        headerRow.AppendChild(th2);

        // Table body rows
        TableTBodyElement tbody = tagged.CreateTableTBodyElement();
        tableElem.AppendChild(tbody);
        for (int i = 1; i <= 20; i++)
        {
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText($"Item {i}");
            bodyRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText($"Value {i}");
            bodyRow.AppendChild(td2);
        }

        // Save the resulting PDF
        doc.Save(outputPath);
    }
}