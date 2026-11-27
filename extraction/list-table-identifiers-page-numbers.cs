using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "tables_list.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare the output file
            using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
            {
                // Check if the document contains tagged content (required for table structure elements)
                // Document.IsTagged does not exist – use TaggedContent null‑check instead.
                if (doc.TaggedContent == null)
                {
                    writer.WriteLine("The document is not tagged; no table structure elements are available.");
                    Console.WriteLine("Document is not tagged. Output file contains a notice.");
                    return;
                }

                // Access the root of the logical structure tree
                StructureElement root = doc.TaggedContent.RootElement;

                // Find all TableElement objects recursively in the structure tree
                var tables = root.FindElements<TableElement>(true);

                if (tables.Count == 0)
                {
                    writer.WriteLine("No tables were found in the document.");
                    Console.WriteLine("No tables detected.");
                }
                else
                {
                    foreach (TableElement table in tables)
                    {
                        // Use AlternativeText as the identifier if it is set; otherwise fallback to a generic name
                        string identifier = !string.IsNullOrEmpty(table.AlternativeText)
                                            ? table.AlternativeText
                                            : "(Unnamed Table)";

                        // TableElement does not expose a PageNumber property in recent versions.
                        // As a fallback we report "unknown" – the page number can be derived via custom logic if needed.
                        string pageInfo = "unknown";

                        writer.WriteLine($"{identifier}: Page {pageInfo}");
                    }

                    Console.WriteLine($"Table list written to '{outputTxtPath}'.");
                }
            }
        }
    }
}
