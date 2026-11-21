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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates structure if not present)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language and title (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element
            StructureElement root = taggedContent.RootElement;

            // Create a table element
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample table with repeating columns";
            table.RepeatingColumnsCount = 3; // repeat first three columns on each page

            // Append the table to the root
            root.AppendChild(table);

            // ----- Header row -----
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Create three header cells (these will be repeated)
            for (int i = 1; i <= 5; i++)
            {
                TableTHElement th = taggedContent.CreateTableTHElement();
                th.SetText($"Header {i}");
                headerRow.AppendChild(th);
            }

            // ----- Body rows -----
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add a few data rows
            for (int row = 1; row <= 4; row++)
            {
                TableTRElement dataRow = taggedContent.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                for (int col = 1; col <= 5; col++)
                {
                    TableTDElement td = taggedContent.CreateTableTDElement();
                    td.SetText($"R{row}C{col}");
                    dataRow.AppendChild(td);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table to '{outputPath}'.");
    }
}