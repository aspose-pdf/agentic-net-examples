// AsposePdfApi.GlobalUsings.g.cs
global using System;
global using System.IO;
global using Aspose.Pdf;
global using Aspose.Pdf.Text;

// Program.cs
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the PDF generated from XML
        const string pdfPath = "input.pdf";

        // Optional: path to save the extracted plain text (for indexing)
        const string txtOutputPath = "extracted.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Create a TextAbsorber to extract text from the whole document
            TextAbsorber absorber = new TextAbsorber
            {
                // Use pure text extraction (no formatting) – suitable for indexing
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Apply the absorber to all pages
            doc.Pages.Accept(absorber);

            // The extracted plain text
            string plainText = absorber.Text;

            // Output to console (optional)
            Console.WriteLine("Extracted text:");
            Console.WriteLine(plainText);

            // Save the extracted text to a file for indexing pipelines
            try
            {
                File.WriteAllText(txtOutputPath, plainText);
                Console.WriteLine($"Plain text saved to '{txtOutputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to write text file: {ex.Message}");
            }
        }
    }
}
