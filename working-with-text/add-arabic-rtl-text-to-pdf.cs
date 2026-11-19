using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rtl_output.pdf";
        const string arabicText = "مرحبا بالعالم"; // Arabic sample text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment containing Arabic text
            TextFragment fragment = new TextFragment(arabicText);

            // Use a Unicode font that supports Arabic glyphs; Aspose.Pdf will render RTL automatically
            fragment.TextState.Font = FontRepository.FindFont("Arial Unicode MS");

            // Add the fragment to the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with RTL text to '{outputPath}'.");
    }
}
