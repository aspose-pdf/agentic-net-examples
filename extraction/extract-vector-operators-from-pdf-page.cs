using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators; // Operator types are defined here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "vector_operators.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 15 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 15)
            {
                Console.Error.WriteLine("The document does not contain page 15.");
                return;
            }

            // Get page 15
            Page page = doc.Pages[15];

            // Open a StreamWriter to write the plain‑text output
            using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
            {
                // The page's content stream is represented by an OperatorCollection
                OperatorCollection ops = page.Contents;

                // Iterate over each operator in the collection (1‑based indexing)
                for (int i = 1; i <= ops.Count; i++)
                {
                    Operator op = ops[i];
                    // Operator.ToString() returns the PDF operator name and its operands
                    writer.WriteLine($"Operator {i}: {op}");
                }
            }
        }

        Console.WriteLine($"Vector operator sequences have been written to '{outputTxtPath}'.");
    }
}
