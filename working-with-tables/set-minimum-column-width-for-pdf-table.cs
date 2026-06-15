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
        const string outputPath = "output.pdf";
        const float minColumnWidth = 50f; // Minimum width in points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates a tagged structure if not present)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with Minimum Column Width");

            // Root element of the tagged structure
            StructureElement root = tagged.RootElement;

            // Create a table element
            TableElement table = tagged.CreateTableElement();

            // Set the default column width to enforce a minimum width (points)
            table.DefaultColumnWidth = minColumnWidth.ToString();

            // Optionally define explicit column widths (three columns as an example)
            table.ColumnWidths = "100 100 100";

            // Attach the table to the document structure
            root.AppendChild(table);

            // ----- Header row -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column 1");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column 2");
            headerRow.AppendChild(th2);

            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Column 3");
            headerRow.AppendChild(th3);

            // ----- Body row -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Data 1");
            bodyRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Data 2");
            bodyRow.AppendChild(td2);

            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("Data 3");
            bodyRow.AppendChild(td3);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with minimum column width to '{outputPath}'.");
    }
}