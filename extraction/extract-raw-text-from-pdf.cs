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

        var sb = new StringBuilder();

        // Load PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure extraction to keep raw formatting (preserves original layout)
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Raw);

            // Process each page individually to keep page order explicit
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                var absorber = new TextAbsorber { ExtractionOptions = extractionOptions };
                doc.Pages[i].Accept(absorber);
                sb.Append(absorber.Text);
            }
        }

        // Write the collected text to a plain‑text file using UTF‑8 encoding
        File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}
