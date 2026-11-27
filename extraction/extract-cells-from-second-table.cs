using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "SecondTableCells.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the PDF is tagged by checking TaggedContent (IsTagged does not exist)
            if (doc.TaggedContent == null)
            {
                Console.Error.WriteLine("The PDF is not a tagged document; table structures cannot be accessed.");
                return;
            }

            ITaggedContent taggedContent = doc.TaggedContent;

            // Find all table elements in the document (recursive search)
            IList<TableElement> tables = taggedContent.RootElement.FindElements<TableElement>(true);

            if (tables.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second table.");
                return;
            }

            // Get the second table (zero‑based index)
            TableElement secondTable = tables[1];

            // Collect cell texts
            List<string> cellTexts = new List<string>();

            // Helper local function to process a table section (thead, tbody, tfoot)
            void ProcessSection(StructureElement section)
            {
                foreach (Element rowElem in section.ChildElements)
                {
                    if (rowElem is TableTRElement row)
                    {
                        foreach (Element cellElem in row.ChildElements)
                        {
                            // Both TH and TD inherit from StructureElement
                            if (cellElem is StructureElement cell)
                            {
                                // ActualText holds the cell's displayed text
                                string text = cell.ActualText ?? string.Empty;
                                cellTexts.Add(text);
                            }
                        }
                    }
                }
            }

            // Process possible sections of the table
            foreach (Element child in secondTable.ChildElements)
            {
                // Sections are TableTHeadElement, TableTBodyElement, TableTFootElement
                if (child is StructureElement section)
                {
                    ProcessSection(section);
                }
            }

            // Write all cell texts to the output file, one per line
            File.WriteAllLines(outputTxt, cellTexts);
            Console.WriteLine($"Extracted {cellTexts.Count} cell texts to '{outputTxt}'.");
        }
    }
}
