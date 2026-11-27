using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "TablesCount.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Use TableAbsorber to detect tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // TableAbsorber.Visit works on a single Page, so iterate through all pages
            foreach (Page page in doc.Pages)
            {
                absorber.Visit(page);
            }

            // absorber.TableList contains all detected tables
            int tablesCount = absorber.TableList.Count;

            // Write the count to a text file
            using (StreamWriter writer = new StreamWriter(outputTxt, false))
            {
                writer.WriteLine(tablesCount);
            }

            Console.WriteLine($"Detected tables: {tablesCount} (written to {outputTxt})");
        }
    }
}
