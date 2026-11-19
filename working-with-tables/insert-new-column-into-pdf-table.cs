using System;
using System.IO;
using System.Linq;
using System.Collections;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    columnIndex = 2; // zero‑based position where the new column will be inserted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Access the tagged content (required for logical structure manipulation)
            Aspose.Pdf.Tagged.ITaggedContent tagged = doc.TaggedContent;

            // Find the first TableElement in the document (adjust as needed for your PDF)
            var tables = tagged.RootElement.FindElements<Aspose.Pdf.LogicalStructure.TableElement>(true);
            if (tables.Count == 0)
            {
                Console.WriteLine("No table found in the document.");
                return;
            }

            Aspose.Pdf.LogicalStructure.TableElement table = tables[0];

            // ----- Header (if present) -----
            var thead = table.ChildElements
                             .OfType<Aspose.Pdf.LogicalStructure.TableTHeadElement>()
                             .FirstOrDefault();

            if (thead != null)
            {
                foreach (Aspose.Pdf.LogicalStructure.TableTRElement headerRow in thead.ChildElements
                                                                                 .OfType<Aspose.Pdf.LogicalStructure.TableTRElement>())
                {
                    // Create a new header cell (TH)
                    Aspose.Pdf.LogicalStructure.TableTHElement newHeader = tagged.CreateTableTHElement();
                    newHeader.SetText("New Header");

                    // Insert the new header cell at the requested column index
                    InsertCellAt(headerRow, newHeader, columnIndex);
                }
            }

            // ----- Body rows -----
            var tbody = table.ChildElements
                             .OfType<Aspose.Pdf.LogicalStructure.TableTBodyElement>()
                             .FirstOrDefault();

            if (tbody != null)
            {
                foreach (Aspose.Pdf.LogicalStructure.TableTRElement bodyRow in tbody.ChildElements
                                                                               .OfType<Aspose.Pdf.LogicalStructure.TableTRElement>())
                {
                    // Create a new data cell (TD)
                    Aspose.Pdf.LogicalStructure.TableTDElement newCell = tagged.CreateTableTDElement();
                    newCell.SetText("New Cell");

                    // Insert the new data cell at the requested column index
                    InsertCellAt(bodyRow, newCell, columnIndex);
                }
            }

            // ----- Footer (if present) -----
            var tfoot = table.ChildElements
                             .OfType<Aspose.Pdf.LogicalStructure.TableTFootElement>()
                             .FirstOrDefault();

            if (tfoot != null)
            {
                foreach (Aspose.Pdf.LogicalStructure.TableTRElement footRow in tfoot.ChildElements
                                                                               .OfType<Aspose.Pdf.LogicalStructure.TableTRElement>())
                {
                    Aspose.Pdf.LogicalStructure.TableTDElement newCell = tagged.CreateTableTDElement();
                    newCell.SetText("New Cell");
                    InsertCellAt(footRow, newCell, columnIndex);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Table column inserted. Output saved to '{outputPath}'.");
        }
    }

    // Helper method that inserts a cell (TH or TD) into a table row at the specified index.
    // If the index is out of range, the cell is appended at the end.
    static void InsertCellAt(Aspose.Pdf.LogicalStructure.TableTRElement row,
                             Aspose.Pdf.LogicalStructure.StructureElement cell,
                             int index)
    {
        // The ChildElements collection implements IList, allowing Insert.
        IList children = (IList)row.ChildElements;

        if (index >= 0 && index < children.Count)
        {
            children.Insert(index, cell);
        }
        else
        {
            // Fallback – append at the end if the index is invalid.
            row.AppendChild(cell);
        }
    }
}