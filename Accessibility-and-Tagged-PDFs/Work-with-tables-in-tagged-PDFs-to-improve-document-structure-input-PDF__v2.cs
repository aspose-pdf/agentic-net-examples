using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;                 // TableAbsorber, AbsorbedTable
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, TableElement, etc.
using Aspose.Pdf.Facades;              // Included as requested (not directly used)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Locate tables on this page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables detected on the page.");
                doc.Save(outputPath);
                return;
            }

            // Take the first detected table
            AbsorbedTable oldTable = absorber.TableList[0];

            // Build a new visual table (you can customize its content/appearance)
            Table newTable = new Table
            {
                ColumnWidths = "100 100 100"
            };
            Row row = newTable.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Replace the original table with the new one
            absorber.Replace(page, oldTable, newTable);

            // ---------- Enhance logical structure (tagged PDF) ----------
            ITaggedContent tagged = doc.TaggedContent;

            // Set document‑level metadata for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with improved table structure");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a tagged TableElement and describe it
            TableElement taggedTable = tagged.CreateTableElement();
            taggedTable.AlternativeText = "Improved table description";
            taggedTable.ColumnWidths = "100 100 100";

            // ----- Table Header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            taggedTable.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Header 1");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Header 2");
            headerRow.AppendChild(th2);

            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Header 3");
            headerRow.AppendChild(th3);

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            taggedTable.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Cell 1");
            bodyRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Cell 2");
            bodyRow.AppendChild(td2);

            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("Cell 3");
            bodyRow.AppendChild(td3);

            // Attach the tagged table to the document's logical structure
            root.AppendChild(taggedTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}