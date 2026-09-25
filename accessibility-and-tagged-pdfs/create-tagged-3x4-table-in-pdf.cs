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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and obtain its tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            // Set language and title for accessibility (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with three rows and four columns.";
            root.AppendChild(table);

            // ----- Header row (first row) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Four header cells
            for (int col = 1; col <= 4; col++)
            {
                TableTHElement th = tagged.CreateTableTHElement();
                th.SetText($"Header {col}");
                headerRow.AppendChild(th);
            }

            // ----- Body rows (second and third rows) -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            for (int row = 1; row <= 2; row++) // two data rows = total 3 rows
            {
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                for (int col = 1; col <= 4; col++)
                {
                    TableTDElement td = tagged.CreateTableTDElement();
                    td.SetText($"R{row}C{col}");
                    dataRow.AppendChild(td);
                }
            }

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}