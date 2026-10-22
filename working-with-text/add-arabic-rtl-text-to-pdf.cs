using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_rtl.pdf";
        const string arabicText = "مرحبا بالعالم"; // Arabic sample text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment containing Arabic text
            TextFragment tf = new TextFragment(arabicText);

            // Use a Unicode font that supports Arabic characters; Aspose.Pdf renders RTL automatically
            tf.TextState.Font = FontRepository.FindFont("Arial Unicode MS");
            tf.TextState.FontSize = 12; // optional size

            // Add the fragment to the first page
            doc.Pages[1].Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RTL PDF saved to '{outputPath}'.");
    }
}
