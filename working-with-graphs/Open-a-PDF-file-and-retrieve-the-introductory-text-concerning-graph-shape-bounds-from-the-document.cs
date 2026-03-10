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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Extract all text from the document
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string allText = absorber.Text;

            // Search for the phrase "graph shape bounds"
            const string keyword = "graph shape bounds";
            int index = allText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase);

            if (index >= 0)
            {
                // Capture a snippet around the keyword (200 chars before and after)
                int start = Math.Max(0, index - 200);
                int length = Math.Min(allText.Length - start, keyword.Length + 400);
                string snippet = allText.Substring(start, length).Trim();

                Console.WriteLine("Introductory text concerning graph shape bounds:");
                Console.WriteLine(snippet);
            }
            else
            {
                Console.WriteLine($"Keyword \"{keyword}\" not found in the document.");
            }
        }
    }
}