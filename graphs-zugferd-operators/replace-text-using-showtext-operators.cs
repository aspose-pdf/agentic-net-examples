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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                OperatorCollection ops = page.Contents;

                // Scan the operator list for ShowText operators
                for (int i = 0; i < ops.Count; i++)
                {
                    if (ops[i] is ShowText show && !string.IsNullOrEmpty(show.Text))
                    {
                        // If the operator contains the target string, replace it
                        if (show.Text.Contains(searchText))
                        {
                            // Simple string replace; you can adjust logic as needed
                            show.Text = show.Text.Replace(searchText, replaceText);
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