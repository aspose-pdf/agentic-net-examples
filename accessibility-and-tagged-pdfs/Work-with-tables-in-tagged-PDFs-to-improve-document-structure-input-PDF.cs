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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content and set basic metadata
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Use TableAbsorber to locate tables in the whole document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc); // extracts tables from all pages

            // Example modification: change text of the first fragment in the first cell of each table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                if (table.RowList.Count > 0 && table.RowList[0].CellList.Count > 0)
                {
                    var cell = table.RowList[0].CellList[0];
                    if (cell.TextFragments.Count > 0)
                    {
                        TextFragment fragment = cell.TextFragments[0];
                        fragment.Text = "Updated cell text";
                    }
                }
            }

            // Create a tagged TableElement that mirrors the first absorbed table
            if (absorber.TableList.Count > 0)
            {
                StructureElement root = tagged.RootElement;

                TableElement taggedTable = tagged.CreateTableElement();
                taggedTable.AlternativeText = "Extracted table with improved structure";
                root.AppendChild(taggedTable);

                // Build table header (use the first row of the absorbed table as header)
                TableTHeadElement thead = tagged.CreateTableTHeadElement();
                taggedTable.AppendChild(thead);
                TableTRElement headerRow = tagged.CreateTableTRElement();
                thead.AppendChild(headerRow);

                var headerAbsorbedRow = absorber.TableList[0].RowList[0];
                foreach (var headerCell in headerAbsorbedRow.CellList)
                {
                    TableTHElement th = tagged.CreateTableTHElement();
                    string headerText = "";
                    foreach (var frag in headerCell.TextFragments)
                        headerText += frag.Text;
                    th.SetText(headerText);
                    headerRow.AppendChild(th);
                }

                // Build table body (remaining rows)
                TableTBodyElement tbody = tagged.CreateTableTBodyElement();
                taggedTable.AppendChild(tbody);
                for (int r = 1; r < absorber.TableList[0].RowList.Count; r++)
                {
                    var bodyAbsorbedRow = absorber.TableList[0].RowList[r];
                    TableTRElement bodyRow = tagged.CreateTableTRElement();
                    tbody.AppendChild(bodyRow);
                    foreach (var bodyCell in bodyAbsorbedRow.CellList)
                    {
                        TableTDElement td = tagged.CreateTableTDElement();
                        string cellText = "";
                        foreach (var frag in bodyCell.TextFragments)
                            cellText += frag.Text;
                        td.SetText(cellText);
                        bodyRow.AppendChild(td);
                    }
                }
            }

            // Save the modified PDF (tagged structure is persisted automatically)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}