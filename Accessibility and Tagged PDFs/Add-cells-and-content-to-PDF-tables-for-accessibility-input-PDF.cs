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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Modify existing table cell text using TableAbsorber
            // ------------------------------------------------------------
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc); // extracts tables from the whole document

            if (absorber.TableList.Count > 0)
            {
                // Example: change the text of the first text fragment in the first cell
                var firstCell = absorber.TableList[0].RowList[0].CellList[0];
                if (firstCell.TextFragments.Count > 0)
                {
                    firstCell.TextFragments[0].Text = "Updated cell text";
                }
            }

            // ------------------------------------------------------------
            // 2. Add a new accessible table via tagged content
            // ------------------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with added accessible table");

            // Get the root element of the logical structure
            StructureElement root = tagged.RootElement;

            // Create a TableElement (tagged table) and attach it to the root
            TableElement tableElem = tagged.CreateTableElement();
            tableElem.AlternativeText = "Sample data table for accessibility";
            root.AppendChild(tableElem);

            // ----- Table Header (THead) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            tableElem.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Header cells (TH)
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Price");
            headerRow.AppendChild(th2);

            // ----- Table Body (TBody) -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            tableElem.AppendChild(tbody);
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            // Data cells (TD)
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Widget A");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("$49.99");
            dataRow.AppendChild(td2);

            // Save the modified document (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}