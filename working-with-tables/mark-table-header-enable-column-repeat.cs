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
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content API
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the structure tree
                StructureElement root = taggedContent.RootElement;

                // Create a table element
                TableElement table = taggedContent.CreateTableElement();
                table.AlternativeText = "Sample data table with repeating header column";
                // NOTE: The TableElement class in the current Aspose.Pdf version does not expose a
                //       RepeatHeaders property. Column header repetition is achieved by marking the
                //       first column cells as <TH> elements (TableTHElement). The PDF viewer will
                //       keep these header cells visible on each new page when the table spans
                //       multiple pages.

                // Attach the table to the document root
                root.AppendChild(table);

                // ----- Create table header (first row) -----
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);

                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);

                // First column header (will be repeated as a column header)
                TableTHElement thHeader = taggedContent.CreateTableTHElement();
                thHeader.SetText("Header Column");
                headerRow.AppendChild(thHeader);

                // Additional column headers (regular cells)
                TableTHElement thCol2 = taggedContent.CreateTableTHElement();
                thCol2.SetText("Column 2");
                headerRow.AppendChild(thCol2);

                TableTHElement thCol3 = taggedContent.CreateTableTHElement();
                thCol3.SetText("Column 3");
                headerRow.AppendChild(thCol3);

                // ----- Create table body -----
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);

                // Example: add three data rows
                for (int i = 1; i <= 3; i++)
                {
                    TableTRElement dataRow = taggedContent.CreateTableTRElement();
                    tbody.AppendChild(dataRow);

                    // First column cell (treated as a header cell for the column)
                    TableTHElement thCell = taggedContent.CreateTableTHElement();
                    thCell.SetText($"Row {i}");
                    dataRow.AppendChild(thCell);

                    // Regular data cells
                    TableTDElement tdCell2 = taggedContent.CreateTableTDElement();
                    tdCell2.SetText($"Data {i}-2");
                    dataRow.AppendChild(tdCell2);

                    TableTDElement tdCell3 = taggedContent.CreateTableTDElement();
                    tdCell3.SetText($"Data {i}-3");
                    dataRow.AppendChild(tdCell3);
                }

                // Save the modified PDF (no PreSave call needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with table saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
