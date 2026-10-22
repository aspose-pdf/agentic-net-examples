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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and obtain its tagged content (required for structure manipulation)
        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF is tagged. The Document.IsTagged property does not exist in recent
            // versions of Aspose.Pdf, so we check the TaggedContent property instead.
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. Tagging is required for table structure manipulation.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            if (root == null)
            {
                Console.WriteLine("The document does not contain a logical structure tree.");
                return;
            }

            // Find the first TableElement in the structure (for demo purposes)
            TableElement oldTable = null;
            if (root.ChildElements != null)
            {
                foreach (Element child in root.ChildElements)
                {
                    if (child is TableElement tbl)
                    {
                        oldTable = tbl;
                        break;
                    }
                }
            }

            if (oldTable == null)
            {
                Console.WriteLine("No table found in the document.");
                return;
            }

            // Create a new table that will replace the old merged cell with separate cells
            TableElement newTable = tagged.CreateTableElement();
            newTable.AlternativeText = "Replaced table with separate cells";

            // Append the new table to the root (or to the same position as the old one)
            root.AppendChild(newTable);

            // ---- Build table header (optional) ----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            newTable.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column 1");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column 2");
            headerRow.AppendChild(th2);

            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Column 3");
            headerRow.AppendChild(th3);

            // ---- Build table body ----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            newTable.AppendChild(tbody);

            // Row 1 (originally had a merged cell spanning columns 2 and 3)
            TableTRElement row1 = tagged.CreateTableTRElement();
            tbody.AppendChild(row1);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Row1-Col1");
            row1.AppendChild(td1);

            // Replace merged cell with two separate cells
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Row1-Col2");
            row1.AppendChild(td2);

            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("Row1-Col3");
            row1.AppendChild(td3);

            // Row 2 (normal row with three cells)
            TableTRElement row2 = tagged.CreateTableTRElement();
            tbody.AppendChild(row2);

            TableTDElement r2c1 = tagged.CreateTableTDElement();
            r2c1.SetText("Row2-Col1");
            row2.AppendChild(r2c1);

            TableTDElement r2c2 = tagged.CreateTableTDElement();
            r2c2.SetText("Row2-Col2");
            row2.AppendChild(r2c2);

            TableTDElement r2c3 = tagged.CreateTableTDElement();
            r2c3.SetText("Row2-Col3");
            row2.AppendChild(r2c3);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
