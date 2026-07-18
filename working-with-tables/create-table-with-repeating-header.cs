using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for visual content)
            Page page = doc.Pages.Add();

            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with Repeating Header");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // Create the table header (THead) group – this group is automatically repeated on each new page
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row inside the THead
            TableTRElement headerRow = tagged.CreateTableTRElement();
            // No IsHeader property exists; the THead container ensures the row repeats on page breaks
            thead.AppendChild(headerRow);

            // Add column header cells (TH elements)
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Quantity");
            headerRow.AppendChild(th2);

            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Price");
            headerRow.AppendChild(th3);

            // Create a table body (TBody) group
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add a few data rows
            for (int i = 1; i <= 20; i++)
            {
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement td1 = tagged.CreateTableTDElement();
                td1.SetText($"Item {i}");
                dataRow.AppendChild(td1);

                TableTDElement td2 = tagged.CreateTableTDElement();
                td2.SetText((i * 2).ToString());
                dataRow.AppendChild(td2);

                TableTDElement td3 = tagged.CreateTableTDElement();
                td3.SetText($"${i * 3.99:F2}");
                dataRow.AppendChild(td3);
            }

            // Save the PDF
            doc.Save("TableWithRepeatingHeader.pdf");
        }

        Console.WriteLine("PDF with repeating table header saved as 'TableWithRepeatingHeader.pdf'.");
    }
}