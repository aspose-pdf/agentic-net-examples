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
        const string searchText = "hello world";   // text to find
        const string replaceText = "hi world";     // optional replacement
        const string fontName = "Arial";           // desired font

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Locate the font to be applied (correct namespace is Aspose.Pdf.Text)
            Font newFont = FontRepository.FindFont(fontName);

            // Create an absorber that searches for the specified phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);

            // Search the entire document (all pages)
            doc.Pages.Accept(absorber);

            // Iterate over each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Optionally replace the text content
                fragment.Text = replaceText;

                // Apply the new font while keeping other layout properties unchanged
                fragment.TextState.Font = newFont;
                // Font size is left as is to preserve layout integrity
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
