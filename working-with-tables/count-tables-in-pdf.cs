using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber extracts tables from the document
            TableAbsorber absorber = new TableAbsorber();

            // Apply the absorber to the whole document (or you can call absorber.Visit(doc.Pages[i]) per page)
            absorber.Visit(doc);

            // The TableList collection holds all detected tables
            int tableCount = absorber.TableList.Count;

            Console.WriteLine($"Number of tables found: {tableCount}");
        }
    }
}