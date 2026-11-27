using System;
using System.IO;
using System.Linq;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangular region (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            // Coordinates are in points (1 point = 1/72 inch)
            Rectangle region = new Rectangle(100, 500, 400, 700);

            // Create a TextFragmentAbsorber and limit extraction to the defined rectangle
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.TextSearchOptions = new TextSearchOptions(region);

            // Extract text from all pages within the specified rectangle
            doc.Pages.Accept(absorber);

            // Concatenate the extracted fragments preserving line breaks
            string extractedText = string.Join(Environment.NewLine,
                absorber.TextFragments.Select(tf => tf.Text));

            // Save the extracted text to a plain‑text file
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Extracted text saved to '{outputPath}'.");
    }
}
