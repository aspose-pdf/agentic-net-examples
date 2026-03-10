using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputFoPath = "input.fo";          // XSL‑FO source file
        const string phraseToFind = "Sample Text";      // Text to search for

        if (!File.Exists(inputFoPath))
        {
            Console.Error.WriteLine($"File not found: {inputFoPath}");
            return;
        }

        // Load the XSL‑FO file as a PDF document (in‑memory)
        using (Document pdfDoc = new Document(inputFoPath, new XslFoLoadOptions()))
        {
            // Create a TextFragmentAbsorber for the desired phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phraseToFind);

            // Perform the search on the whole document
            absorber.Visit(pdfDoc);

            // Output each found fragment with its page number and position
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"Found \"{fragment.Text}\" on page {fragment.Page.Number}");
                Aspose.Pdf.Text.Position pos = fragment.Position;
                Console.WriteLine($"  Position: X = {pos.XIndent}, Y = {pos.YIndent}");
            }

            // Optional: save the PDF if further processing is required
            // pdfDoc.Save("output.pdf");
        }
    }
}