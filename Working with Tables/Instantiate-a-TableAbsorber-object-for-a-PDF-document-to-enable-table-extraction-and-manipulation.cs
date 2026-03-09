using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Instantiate a TableAbsorber to locate tables in the document
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Extract tables from the entire document (alternatively, use Visit(doc.Pages[1]) for a single page)
            tableAbsorber.Visit(doc);

            // Example manipulation: if at least one table is found, change the text of the first text fragment in the first cell
            if (tableAbsorber.TableList.Count > 0 &&
                tableAbsorber.TableList[0].RowList.Count > 0 &&
                tableAbsorber.TableList[0].RowList[0].CellList.Count > 0 &&
                tableAbsorber.TableList[0].RowList[0].CellList[0].TextFragments.Count > 1)
            {
                TextFragment fragment = tableAbsorber.TableList[0].RowList[0].CellList[0].TextFragments[1];
                fragment.Text = "Modified text";
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table extraction and manipulation completed. Output saved to '{outputPath}'.");
    }
}