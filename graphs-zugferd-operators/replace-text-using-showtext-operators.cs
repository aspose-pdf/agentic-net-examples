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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                // In recent Aspose.Pdf versions the low‑level operators are accessed via the Contents collection
                OperatorCollection contents = page.Contents;

                // Iterate over the low‑level operators on the page (1‑based indexing)
                for (int opIndex = 1; opIndex <= contents.Count; opIndex++)
                {
                    // Identify ShowText (Tj) operators
                    if (contents[opIndex] is ShowText showText)
                    {
                        // Replace occurrences of the target string
                        if (!string.IsNullOrEmpty(showText.Text) && showText.Text.Contains(searchText))
                        {
                            // Create a new ShowText operator with the replaced text
                            ShowText newOp = new ShowText(showText.Text.Replace(searchText, replaceText));
                            // Substitute the old operator with the new one
                            contents[opIndex] = newOp;
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}
