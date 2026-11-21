using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "underlined_output.pdf";
        const string sampleText = "Underlined text example";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Create a text fragment with the desired content
                TextFragment fragment = new TextFragment(sampleText);

                // Enable underline via the TextState
                fragment.TextState.Underline = true;

                // Optional: set the position of the fragment on the page
                fragment.Position = new Position(100, 700);

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(fragment);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Underlined text added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}