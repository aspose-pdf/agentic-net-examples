using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "FirstTableCells.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the PDF is tagged; otherwise table structure is unavailable
            if (doc.TaggedContent == null)
            {
                Console.Error.WriteLine("The PDF is not a tagged document; no table structure to extract.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Find the first TableElement in the structure tree
            TableElement firstTable = root.FindElements<TableElement>(true).FirstOrDefault();
            if (firstTable == null)
            {
                Console.Error.WriteLine("No table found in the document.");
                return;
            }

            // Prepare a list to hold each row's cell texts
            List<string> rowsText = new List<string>();

            // Helper to process a table section (thead, tbody, tfoot)
            void ProcessSection(Element section)
            {
                // Section elements contain TableTRElement rows
                foreach (Element rowElem in section.ChildElements)
                {
                    if (rowElem is TableTRElement row)
                    {
                        List<string> cellTexts = new List<string>();
                        // Cells are TableTHElement (header) or TableTDElement (data)
                        foreach (Element cellElem in row.ChildElements)
                        {
                            if (cellElem is TableTHElement th)
                                cellTexts.Add(th.ActualText ?? string.Empty);
                            else if (cellElem is TableTDElement td)
                                cellTexts.Add(td.ActualText ?? string.Empty);
                        }
                        // Join cells with a tab delimiter
                        rowsText.Add(string.Join("\t", cellTexts));
                    }
                }
            }

            // Process possible sections of the table (thead, tbody, tfoot)
            foreach (Element section in firstTable.ChildElements)
            {
                ProcessSection(section);
            }

            // Write the extracted rows to the output text file
            File.WriteAllLines(outputTxt, rowsText);
            Console.WriteLine($"First table cells written to '{outputTxt}'.");
        }
    }
}
