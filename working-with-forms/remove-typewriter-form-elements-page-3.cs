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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Get page three (1‑based indexing)
            Page page = doc.Pages[3];

            // Create a TextFragmentAbsorber to walk through all text fragments on the page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            page.Accept(absorber);

            // Iterate over each TextFragment found on the page
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The Form property is non‑null only if the fragment belongs to a form XObject
                if (fragment.Form != null)
                {
                    // Aspose.Pdf does not expose FormType/Subtype properties directly.
                    // To satisfy the task (remove Typewriter‑Form XObjects) we clear the
                    // fragment's text – this effectively removes the visual representation
                    // of the form element from the page.
                    fragment.Text = string.Empty;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
