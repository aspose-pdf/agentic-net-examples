using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";

        // Verify the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAbsorber instance
            TextAbsorber absorber = new TextAbsorber();

            // Configure ExtractionOptions to preserve paragraph structure.
            // Using Pure formatting mode keeps line breaks which can be used to identify paragraphs.
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // The extracted text contains line breaks; split into paragraphs.
            string[] paragraphs = absorber.Text.Split(new[] { "\r\n\r\n", "\n\n", "\r\r" }, StringSplitOptions.RemoveEmptyEntries);

            // Output paragraphs to console
            Console.WriteLine("Extracted Paragraphs:");
            for (int i = 0; i < paragraphs.Length; i++)
            {
                Console.WriteLine($"--- Paragraph {i + 1} ---");
                Console.WriteLine(paragraphs[i].Trim());
                Console.WriteLine();
            }

            // (Optional) Save the extracted text to a file
            string outputTextPath = "extracted_text.txt";
            File.WriteAllText(outputTextPath, absorber.Text);
            Console.WriteLine($"Full extracted text saved to '{outputTextPath}'.");
        }
    }
}