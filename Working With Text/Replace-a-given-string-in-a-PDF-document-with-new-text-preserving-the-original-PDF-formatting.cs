using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReplaceTextInPdf
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Text to search for and its replacement
        const string searchText  = "old string";
        const string replaceText = "new string";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that will locate all occurrences of the search text
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);

            // Search each page of the document
            foreach (Page page in doc.Pages)
            {
                page.Accept(absorber);
            }

            // Replace the found text while preserving original formatting (font, size, color, etc.)
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replaceText;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}