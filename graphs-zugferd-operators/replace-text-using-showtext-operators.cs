using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldText    = "Hello";
        const string newText    = "Hi";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // OperatorCollection holds all content operators for the page
                var operators = page.Contents;

                // Scan the operator list and replace matching ShowText operators
                for (int i = 0; i < operators.Count; i++)
                {
                    if (operators[i] is ShowText show && show.Text != null && show.Text.Contains(oldText))
                    {
                        // Create a new ShowText operator with the replaced string
                        ShowText replacement = new ShowText(show.Text.Replace(oldText, newText));

                        // Preserve the original operator index (required by the PDF content stream)
                        replacement.Index = show.Index;

                        // Substitute the operator in the collection
                        operators[i] = replacement;
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}