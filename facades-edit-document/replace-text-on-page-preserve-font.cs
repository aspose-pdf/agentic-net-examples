using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcString  = "Old Text";   // text to replace
        const string destString = "New Text";   // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Locate the source text on page 2 to capture its original TextState (font, size, color, etc.)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(srcString);
            // Accept the absorber for the specific page (pages are 1‑based)
            doc.Pages[2].Accept(absorber);

            // If the text is found, retrieve its TextState from the first occurrence
            TextState originalState = null;
            if (absorber.TextFragments.Count > 0)
            {
                // TextState contains font, size, color, etc.
                originalState = absorber.TextFragments[1].TextState;
            }
            else
            {
                Console.Error.WriteLine($"Source text \"{srcString}\" not found on page 2.");
                // Still proceed with a default style if needed, or exit
                return;
            }

            // Create a PdfContentEditor facade and bind it to the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace the text on page 2 using the captured TextState to preserve original styling
            bool replaced = editor.ReplaceText(srcString, 2, destString, originalState);
            if (!replaced)
            {
                Console.Error.WriteLine("Replacement operation failed.");
            }

            // Save the modified document (using the same path as required)
            doc.Save(outputPath);
            // Close the editor (optional, as it does not hold unmanaged resources)
            editor.Close();
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}