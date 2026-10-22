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
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (creates if missing) and set basic properties
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage("en-US");
                tagged.SetTitle("Document with Double-Bordered Table");

                // Root element of the logical structure tree
                StructureElement root = tagged.RootElement;

                // Create a table element and set its description
                TableElement table = tagged.CreateTableElement();
                table.AlternativeText = "Sample data table with double border";

                // Apply double‑border like appearance by using a thick border on all sides
                // BorderInfo does not expose a Style property; a double border can be mimicked
                // with a wider line. Use the constructor to set side, width and (optional) color.
                table.Border = new BorderInfo(BorderSide.All, 2f);

                // Attach the table to the root element
                root.AppendChild(table);

                // ----- Header row -----
                TableTHeadElement thead = tagged.CreateTableTHeadElement();
                table.AppendChild(thead);
                TableTRElement headerRow = tagged.CreateTableTRElement();
                thead.AppendChild(headerRow);

                TableTHElement th1 = tagged.CreateTableTHElement();
                th1.SetText("Product");
                headerRow.AppendChild(th1);

                TableTHElement th2 = tagged.CreateTableTHElement();
                th2.SetText("Price");
                headerRow.AppendChild(th2);

                // ----- Body row -----
                TableTBodyElement tbody = tagged.CreateTableTBodyElement();
                table.AppendChild(tbody);
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement td1 = tagged.CreateTableTDElement();
                td1.SetText("Widget A");
                dataRow.AppendChild(td1);

                TableTDElement td2 = tagged.CreateTableTDElement();
                td2.SetText("$25.00");
                dataRow.AppendChild(td2);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with double‑bordered table: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
