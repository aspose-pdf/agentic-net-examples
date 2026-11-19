using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class ReplaceMergedCell
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_replaced_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (required for structure manipulation)
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the structure tree
                StructureElement root = taggedContent.RootElement;

                // OPTIONAL: remove existing tables (if any) by not adding them to the new document.
                // For simplicity we just add a new table; existing tables remain unchanged.

                // Create a new table element
                TableElement table = taggedContent.CreateTableElement();
                table.AlternativeText = "Table with replaced cells (no merged cells)";
                root.AppendChild(table);

                // ----- Create table header -----
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);

                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);

                TableTHElement th1 = taggedContent.CreateTableTHElement();
                th1.SetText("Column 1");
                headerRow.AppendChild(th1);

                TableTHElement th2 = taggedContent.CreateTableTHElement();
                th2.SetText("Column 2");
                headerRow.AppendChild(th2);

                TableTHElement th3 = taggedContent.CreateTableTHElement();
                th3.SetText("Column 3");
                headerRow.AppendChild(th3);

                // ----- Create table body -----
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);

                // Example data rows – each cell is a separate TableTDElement (no merged cells)
                for (int i = 1; i <= 3; i++)
                {
                    TableTRElement dataRow = taggedContent.CreateTableTRElement();
                    tbody.AppendChild(dataRow);

                    TableTDElement td1 = taggedContent.CreateTableTDElement();
                    td1.SetText($"R{i}C1");
                    dataRow.AppendChild(td1);

                    TableTDElement td2 = taggedContent.CreateTableTDElement();
                    td2.SetText($"R{i}C2");
                    dataRow.AppendChild(td2);

                    TableTDElement td3 = taggedContent.CreateTableTDElement();
                    td3.SetText($"R{i}C3");
                    dataRow.AppendChild(td3);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with replaced table: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}