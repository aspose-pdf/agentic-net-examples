using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int columnToRemove = 1; // zero‑based index of the column to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (creates one if the document is not already tagged)
                ITaggedContent tagged = doc.TaggedContent;

                // Find all tables in the document
                var tables = tagged.RootElement.FindElements<TableElement>(true);
                if (tables.Count == 0)
                {
                    Console.WriteLine("No table found in the document.");
                }
                else
                {
                    foreach (TableElement table in tables)
                    {
                        // Helper to process rows inside a container (thead, tbody, tfoot)
                        void ProcessRows(StructureElement container)
                        {
                            foreach (Element rowElem in container.ChildElements)
                            {
                                if (rowElem is TableTRElement row)
                                {
                                    // Convert cells to a list for indexed access
                                    var cells = row.ChildElements.Cast<Element>().ToList();
                                    if (columnToRemove >= 0 && columnToRemove < cells.Count)
                                    {
                                        // Remove the cell at the specified column index
                                        row.RemoveChild(columnToRemove);
                                    }
                                }
                            }
                        }

                        // Header rows
                        var thead = table.ChildElements.OfType<TableTHeadElement>().FirstOrDefault();
                        if (thead != null) ProcessRows(thead);

                        // Body rows
                        var tbody = table.ChildElements.OfType<TableTBodyElement>().FirstOrDefault();
                        if (tbody != null) ProcessRows(tbody);

                        // Footer rows (optional)
                        var tfoot = table.ChildElements.OfType<TableTFootElement>().FirstOrDefault();
                        if (tfoot != null) ProcessRows(tfoot);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Column removed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
