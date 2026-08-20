using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_header.pdf";

        // Load existing PDF or create a new one
        using (Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document())
        {
            // Ensure at least one page exists
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Table Header");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // Create the table header (THead) group
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row inside the THead
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Add header cells (TH) to the header row
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column 1");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column 2");
            headerRow.AppendChild(th2);

            // Instruct the table to repeat the first row on each new page
            table.RepeatingRowsCount = 1;

            // Optional: add a body row to demonstrate the repeating header
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Data A");
            bodyRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Data B");
            bodyRow.AppendChild(td2);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with repeating table header saved.");
    }
}