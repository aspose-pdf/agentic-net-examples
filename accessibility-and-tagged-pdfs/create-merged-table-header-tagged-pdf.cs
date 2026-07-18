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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // Create the table header (THead) and attach it to the table
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row (TR) inside the THead
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // First header cell spans two columns (merged cells)
            TableTHElement mergedHeader = tagged.CreateTableTHElement();
            mergedHeader.ColSpan = 2;                     // Merge across two columns
            mergedHeader.ActualText = "Merged Header";    // Set /ActualText
            headerRow.AppendChild(mergedHeader);

            // Second header cell for the third column
            TableTHElement thirdHeader = tagged.CreateTableTHElement();
            thirdHeader.ActualText = "Third Column";
            headerRow.AppendChild(thirdHeader);

            // (Optional) Add a body row to illustrate the table structure
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            // First data cell (TD)
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Cell 1");
            bodyRow.AppendChild(td1);

            // Second data cell (TD)
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Cell 2");
            bodyRow.AppendChild(td2);

            // Third data cell (TD)
            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("Cell 3");
            bodyRow.AppendChild(td3);

            // Save the modified PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with merged header saved to '{outputPath}'.");
    }
}