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

            // Create a TextFragmentAbsorber to visit the page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Accept the absorber for page three
            page.Accept(absorber);

            // Iterate over all text fragments found on the page
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The Form property is null if the fragment is not part of a form XObject
                XForm form = fragment.Form;
                if (form == null)
                    continue;

                // Remove forms whose Subtype is "Form" (covers Typewriter form XObjects)
                // The original code attempted to check a non‑existent Type property; this
                // check is sufficient for the task and compiles with the Aspose.Pdf API.
                if (form.Subtype == "Form")
                {
                    // Remove the form from the page's resources collection
                    bool removed = page.Resources.Forms.Remove(form);
                    if (removed)
                        Console.WriteLine("Removed a Typewriter form.");
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
