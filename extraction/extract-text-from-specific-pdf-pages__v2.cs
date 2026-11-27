using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use a StringBuilder to collect text from the selected pages.
        StringBuilder extractedText = new StringBuilder();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            int[] pagesToExtract = { 1, 3 };

            foreach (int pageNumber in pagesToExtract)
            {
                // Ensure the requested page exists.
                if (pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} does not exist in the document.");
                    continue;
                }

                // TextAbsorber with default settings extracts raw text.
                TextAbsorber absorber = new TextAbsorber();

                // Apply the absorber to the specific page.
                doc.Pages[pageNumber].Accept(absorber);

                // Append the extracted text for this page.
                extractedText.AppendLine(absorber.Text);
            }
        }

        // Write the collected text to a plain‑text file.
        File.WriteAllText(outputPath, extractedText.ToString());

        Console.WriteLine($"Text from pages 1 and 3 saved to '{outputPath}'.");
    }
}