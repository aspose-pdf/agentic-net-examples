using System;
using System.IO;
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

        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle area for the header (llx, lly, urx, ury)
            var headerRect = new Aspose.Pdf.Rectangle(0, 750, 595, 842);

            // Create a TextAbsorber and restrict extraction to the defined rectangle
            TextAbsorber absorber = new TextAbsorber();
            absorber.TextSearchOptions = new TextSearchOptions(headerRect);

            // Extract text from the first page (1‑based indexing)
            doc.Pages[1].Accept(absorber);

            // Write the extracted text to a plain‑text file
            File.WriteAllText(outputPath, absorber.Text);
        }

        Console.WriteLine($"Header text extracted to '{outputPath}'.");
    }
}
