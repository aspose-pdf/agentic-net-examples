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
        const string keyword = "TARGET";          // text to search for
        const string replacement = "REPLACED";    // new text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Locate all tables in the document
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(doc); // search the whole document

            // Iterate through each found table
            foreach (var absorbedTable in tableAbsorber.TableList)
            {
                // Iterate rows
                foreach (var row in absorbedTable.RowList)
                {
                    // Iterate cells
                    foreach (var cell in row.CellList)
                    {
                        // Each cell may contain several text fragments
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            // If the fragment contains the keyword, replace it
                            if (fragment.Text != null && fragment.Text.Contains(keyword))
                            {
                                fragment.Text = replacement;
                            }
                        }
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}