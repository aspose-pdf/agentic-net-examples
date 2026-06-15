using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, TableElement, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF (can be untagged)
        const string outputPath = "output_tagged.pdf";  // result with table header

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language / title for the whole document
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ------------------------------------------------------------
            // Create a table structure element and attach it to the root
            // ------------------------------------------------------------
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with merged header cells";
            root.AppendChild(table);   // AppendChild with one argument (bool default)

            // ------------------------------------------------------------
            // Create the table header (THead) and a header row (TR)
            // ------------------------------------------------------------
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // ------------------------------------------------------------
            // First header cell – spans two columns (merged cells)
            // ------------------------------------------------------------
            TableTHElement thMerged = tagged.CreateTableTHElement();
            thMerged.SetText("Merged Header");   // Set visible text
            thMerged.ActualText = "Merged Header"; // ActualText for accessibility
            thMerged.ColSpan = 2;                // Merge two columns
            headerRow.AppendChild(thMerged);

            // ------------------------------------------------------------
            // Second header cell – occupies the remaining column
            // ------------------------------------------------------------
            TableTHElement thSingle = tagged.CreateTableTHElement();
            thSingle.SetText("Separate Header");
            thSingle.ActualText = "Separate Header";
            headerRow.AppendChild(thSingle);

            // ------------------------------------------------------------
            // (Optional) Add a body row to illustrate the table structure
            // ------------------------------------------------------------
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            table.AppendChild(bodyRow);

            // First data cell
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Cell 1");
            td1.ActualText = "Cell 1";
            bodyRow.AppendChild(td1);

            // Second data cell (part of the merged column)
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Cell 2");
            td2.ActualText = "Cell 2";
            bodyRow.AppendChild(td2);

            // Third data cell
            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("Cell 3");
            td3.ActualText = "Cell 3";
            bodyRow.AppendChild(td3);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with merged header saved to '{outputPath}'.");
    }
}