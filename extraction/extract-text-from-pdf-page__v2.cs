using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least 8 pages (Aspose.Pdf uses 1‑based indexing)
                if (doc.Pages.Count < 8)
                {
                    Console.Error.WriteLine("The document has fewer than 8 pages.");
                    return;
                }

                // Use TextAbsorber to extract the textual content of page 8
                TextAbsorber absorber = new TextAbsorber();
                absorber.Visit(doc.Pages[8]);

                // Write the extracted text to the output file
                File.WriteAllText(outputPath, absorber.Text, Encoding.UTF8);
            }

            Console.WriteLine($"Text from page 8 has been written to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
