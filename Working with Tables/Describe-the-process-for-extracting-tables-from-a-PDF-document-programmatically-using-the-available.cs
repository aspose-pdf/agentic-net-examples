using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance – it will locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document (alternatively, call absorber.Visit(doc.Pages[1]) for a single page)
            absorber.Visit(doc);

            // Check whether any tables were found
            if (absorber.TableList.Count > 0)
            {
                // Access the first table, its first row, first cell, and the second text fragment within that cell
                TextFragment fragment = absorber.TableList[0].RowList[0].CellList[0].TextFragments[1];

                // Example modification: change the text of the selected fragment
                fragment.Text = "hi world";
            }

            // Save the (potentially modified) PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}