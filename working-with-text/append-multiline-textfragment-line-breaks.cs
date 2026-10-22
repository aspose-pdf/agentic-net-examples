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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define a multi‑line string (\n denotes line breaks)
            string multiLineText = "First line.\nSecond line.\nThird line.";

            // Create a TextFragment with the multi‑line content
            TextFragment textFragment = new TextFragment(multiLineText);

            // Optionally set the position of the fragment on the page
            textFragment.Position = new Position(100, 700);

            // Append the fragment to the page's paragraph collection
            page.Paragraphs.Add(textFragment);

            // Retrieve line‑break information by analysing the original text
            Console.WriteLine("Line‑break information for each line:");
            string[] lines = multiLineText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine($"Line {i}: Text=\"{lines[i]}\"");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
