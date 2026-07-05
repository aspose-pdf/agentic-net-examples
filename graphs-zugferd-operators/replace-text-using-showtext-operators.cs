using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchText = "Hello";
        const string replaceText = "Hi";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                OperatorCollection operators = page.Contents;

                // Scan the operator list for ShowText (Tj) operators
                for (int i = 0; i < operators.Count; i++)
                {
                    if (operators[i] is ShowText showTextOp)
                    {
                        // Check if the operator's text contains the target string
                        if (!string.IsNullOrEmpty(showTextOp.Text) && showTextOp.Text.Contains(searchText))
                        {
                            // Build the replacement string
                            string newText = showTextOp.Text.Replace(searchText, replaceText);

                            // Create a new ShowText operator with the replaced text
                            ShowText newOp = new ShowText(newText)
                            {
                                // Preserve the original operator index (optional but keeps ordering)
                                Index = showTextOp.Index
                            };

                            // Replace the old operator with the new one in the collection
                            operators[i] = newOp;
                        }
                    }
                }
            }

            // Save the modified PDF (PDF format is the default when no SaveOptions are supplied)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}