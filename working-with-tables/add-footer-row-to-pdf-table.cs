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
        const string outputPath = "table_with_footer.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a visual table and add it to the page
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Header row
            Row header = table.Rows.Add();
            header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12
            };
            header.Cells.Add("Product");
            header.Cells.Add("Quantity");
            header.Cells.Add("Price");

            // Body row
            Row body = table.Rows.Add();
            body.Cells.Add("Widget");
            body.Cells.Add("10");
            body.Cells.Add("$5.00");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Set up tagged PDF structure
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with Footer");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Table structure element and attach it to the root
            TableElement tableStruct = tagged.CreateTableElement();
            tableStruct.AlternativeText = "Sample data table";
            root.AppendChild(tableStruct);

            // Create the TFoot (footer) element and attach it to the table
            TableTFootElement tFoot = tagged.CreateTableTFootElement();
            tFoot.AlternativeText = "Table footer row";
            tableStruct.AppendChild(tFoot);

            // Create a row inside the TFoot
            TableTRElement footRow = tFoot.CreateTR();

            // First cell of the footer row
            TableTDElement footCell1 = tagged.CreateTableTDElement();
            footCell1.SetText("Total");
            footRow.AppendChild(footCell1);

            // Second (empty) cell
            TableTDElement footCell2 = tagged.CreateTableTDElement();
            footCell2.SetText(string.Empty);
            footRow.AppendChild(footCell2);

            // Third cell with the total amount
            TableTDElement footCell3 = tagged.CreateTableTDElement();
            footCell3.SetText("$50.00");
            footRow.AppendChild(footCell3);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}