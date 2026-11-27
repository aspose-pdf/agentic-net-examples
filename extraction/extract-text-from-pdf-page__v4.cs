using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";
        const int pageNumber = 24; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Use TextAbsorber (core API) to extract the textual content of the page
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages[pageNumber].Accept(absorber);

            // Write the extracted text to the output file
            File.WriteAllText(outputPath, absorber.Text);

            Console.WriteLine($"Text from page {pageNumber} has been saved to '{outputPath}'.");
        }
    }
}
