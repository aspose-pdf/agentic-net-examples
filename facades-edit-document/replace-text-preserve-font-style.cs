using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldText = "OldString";
        const string newText = "NewString";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Create a TextFragmentAbsorber to find the text on page 2 only
        TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldText);
        // Page collection is 1‑based; page 2 is accessed via index 2
        pdfDocument.Pages[2].Accept(absorber);

        // Replace each found fragment with the new text while preserving original font/style
        foreach (TextFragment fragment in absorber.TextFragments)
        {
            fragment.Text = newText;
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Text replaced and saved to '{outputPath}'.");
    }
}
