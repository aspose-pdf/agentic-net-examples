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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use TextFragmentAbsorber to extract raw text while preserving spacing.
            // The parameter‑less constructor works across Aspose.Pdf versions.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Process each page – the absorber will collect text from all pages.
            foreach (Page page in doc.Pages)
            {
                absorber.Visit(page);
            }

            // Write the extracted text to the output file.
            File.WriteAllText(outputPath, absorber.Text);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}
