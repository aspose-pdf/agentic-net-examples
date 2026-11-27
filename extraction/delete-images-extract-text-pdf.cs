using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputTextPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load PDF and process it
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 7 exists (pages are 1‑based)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("PDF does not contain page 7.");
                return;
            }

            Page page = doc.Pages[7];

            // Delete images at indexes 2 and 4 (1‑based). Remove higher index first.
            // XImageCollection uses 1‑based indexing and provides Delete(index).
            var images = page.Resources.Images;

            if (images.Count >= 4)
            {
                // Remove image at index 4 first, then index 2.
                images.Delete(4);
                images.Delete(2);
            }
            else
            {
                Console.Error.WriteLine("Not enough images on page 7 to delete indexes 2 and 4.");
            }

            // Extract all text from the document
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Save extracted text to a file
            File.WriteAllText(outputTextPath, extractedText);
        }

        Console.WriteLine($"Text extracted and saved to '{outputTextPath}'.");
    }
}
