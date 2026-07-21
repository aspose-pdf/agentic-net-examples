using System;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page (required for tagging)
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("NestedTableExample");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // 1. Paragraph element that will contain the outer table
            // -------------------------------------------------
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("Paragraph containing a table:");
            root.AppendChild(paragraph);

            // -------------------------------------------------
            // 2. Outer table
            // -------------------------------------------------
            TableElement outerTable = tagged.CreateTableElement();
            outerTable.AlternativeText = "Outer table";
            paragraph.AppendChild(outerTable);

            // Header for outer table
            TableTHeadElement outerHead = tagged.CreateTableTHeadElement();
            outerTable.AppendChild(outerHead);
            TableTRElement outerHeaderRow = tagged.CreateTableTRElement();
            outerHead.AppendChild(outerHeaderRow);
            TableTHElement outerHeaderCell1 = tagged.CreateTableTHElement();
            outerHeaderCell1.SetText("Header 1");
            outerHeaderRow.AppendChild(outerHeaderCell1);
            TableTHElement outerHeaderCell2 = tagged.CreateTableTHElement();
            outerHeaderCell2.SetText("Header 2");
            outerHeaderRow.AppendChild(outerHeaderCell2);

            // Body for outer table
            TableTBodyElement outerBody = tagged.CreateTableTBodyElement();
            outerTable.AppendChild(outerBody);
            TableTRElement outerRow = tagged.CreateTableTRElement();
            outerBody.AppendChild(outerRow);

            // First cell (simple text)
            TableTDElement outerCell1 = tagged.CreateTableTDElement();
            outerCell1.SetText("Cell 1");
            outerRow.AppendChild(outerCell1);

            // Second cell – will host the nested table
            TableTDElement outerCell2 = tagged.CreateTableTDElement();
            outerCell2.SetText("Nested Table:");
            outerRow.AppendChild(outerCell2);

            // -------------------------------------------------
            // 3. Inner (nested) table
            // -------------------------------------------------
            TableElement innerTable = tagged.CreateTableElement();
            innerTable.AlternativeText = "Inner table";
            outerCell2.AppendChild(innerTable);

            // Header for inner table
            TableTHeadElement innerHead = tagged.CreateTableTHeadElement();
            innerTable.AppendChild(innerHead);
            TableTRElement innerHeaderRow = tagged.CreateTableTRElement();
            innerHead.AppendChild(innerHeaderRow);
            TableTHElement innerHeaderCell1 = tagged.CreateTableTHElement();
            innerHeaderCell1.SetText("Inner Header 1");
            innerHeaderRow.AppendChild(innerHeaderCell1);
            TableTHElement innerHeaderCell2 = tagged.CreateTableTHElement();
            innerHeaderCell2.SetText("Inner Header 2");
            innerHeaderRow.AppendChild(innerHeaderCell2);

            // Body for inner table
            TableTBodyElement innerBody = tagged.CreateTableTBodyElement();
            innerTable.AppendChild(innerBody);
            TableTRElement innerRow = tagged.CreateTableTRElement();
            innerBody.AppendChild(innerRow);
            TableTDElement innerCell1 = tagged.CreateTableTDElement();
            innerCell1.SetText("Inner Cell 1");
            innerRow.AppendChild(innerCell1);
            TableTDElement innerCell2 = tagged.CreateTableTDElement();
            innerCell2.SetText("Inner Cell 2");
            innerRow.AppendChild(innerCell2);

            // Save the PDF document
            doc.Save("NestedTableExample.pdf");
        }
    }
}
