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
        const string searchText = "Hello World";
        const string replaceText = "Hi Universe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle rule: wrap Document in using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (page indexing is 1‑based)
            foreach (Page page in doc.Pages)
            {
                // OperatorCollection holds low‑level PDF operators for the page
                var operators = page.Contents;

                // Scan the collection for ShowText operators (Tj)
                for (int i = 0; i < operators.Count; i++)
                {
                    if (operators[i] is ShowText showOp && !string.IsNullOrEmpty(showOp.Text))
                    {
                        // If the operator's text contains the target string, replace it
                        if (showOp.Text.Contains(searchText))
                        {
                            // Create a new ShowText operator with the same index but new text
                            ShowText newOp = new ShowText(showOp.Index, showOp.Text.Replace(searchText, replaceText));

                            // Replace the old operator with the new one (OperatorCollection.Replace can also be used)
                            operators[i] = newOp;
                        }
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: Save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}