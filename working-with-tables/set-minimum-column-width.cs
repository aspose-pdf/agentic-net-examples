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
        const string outputPath = "output_mincolwidth.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content (creates a tagged PDF if not already tagged)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element
            TableElement table = tagged.CreateTableElement();

            // Set a description for accessibility
            table.AlternativeText = "Sample data table with minimum column width";

            // Define explicit column widths (in points).
            // Aspose.Pdf.Tagged.TableElement does not expose a direct "minimum width" property.
            // To emulate a minimum width, we set the initial widths to the desired values.
            // These values act as the smallest width the columns will have unless the content forces a larger width.
            table.ColumnWidths = "120 120 120";   // initial widths (points)

            // Attach the table to the document structure
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
            th2.SetText("Quantity");
            headerRow.AppendChild(th2);

            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Price");
            headerRow.AppendChild(th3);

            // ----- Body rows -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Example data row
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Widget A");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("150");
            dataRow.AppendChild(td2);

            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("$12.99");
            dataRow.AppendChild(td3);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with minimum column widths saved to '{outputPath}'.");
    }
}
