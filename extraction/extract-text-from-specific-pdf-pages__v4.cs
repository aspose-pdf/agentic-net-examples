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

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 13 pages (pages are 1‑based)
            if (doc.Pages.Count < 13)
            {
                Console.Error.WriteLine("Document does not contain pages 12 and 13.");
                return;
            }

            // Extract text from page 12
            TextAbsorber absorber12 = new TextAbsorber();
            absorber12.Visit(doc.Pages[12]); // Pages are 1‑based

            // Extract text from page 13
            TextAbsorber absorber13 = new TextAbsorber();
            absorber13.Visit(doc.Pages[13]);

            // Combine extracted texts
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(absorber12.Text);
            sb.AppendLine(absorber13.Text);

            // Save combined text to a plain .txt file
            File.WriteAllText(outputPath, sb.ToString());
        }

        Console.WriteLine($"Extracted text saved to '{outputPath}'.");
    }
}
