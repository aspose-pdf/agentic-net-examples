using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least 15 pages (1‑based indexing)
            if (doc.Pages.Count < 15)
            {
                Console.Error.WriteLine("The document does not contain page 15.");
                return;
            }

            // Use TextAbsorber (core API) to capture the textual content of page 15.
            // ParagraphAbsorber is not available in the core namespace for the referenced version,
            // therefore TextAbsorber is the appropriate replacement for extracting paragraph markup.
            TextAbsorber absorber = new TextAbsorber();
            absorber.Visit(doc.Pages[15]);

            // Write extracted text to the output file. Split by line‑breaks to emulate paragraph separation.
            using (StreamWriter writer = new StreamWriter(outputTxt, false))
            {
                string[] lines = absorber.Text?.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None) ?? Array.Empty<string>();
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine($"Extracted text from page 15 saved to '{outputTxt}'.");
        }
    }
}
