using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: create → load → save)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Get page three (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[3];

            // Create a TextFragmentAbsorber to visit the page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Accept the absorber on the specific page
            page.Accept(absorber);

            // Iterate over all text fragments found on the page
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The Form property returns the XForm that contains the fragment (if any)
                XForm form = fragment.Form;

                // If the fragment belongs to a form XObject, attempt to remove it
                if (form != null)
                {
                    // OPTIONAL: filter by form name or other criteria to match
                    // Typewriter fields with subtype "Form" could be identified by naming convention.
                    // Example placeholder check (replace with real condition if known):
                    // if (form.Name != null && form.Name.Contains("Typewriter"))
                    // {
                    //     // Remove the matching form from the page's form collection
                    //     page.Resources.Forms.Remove(form);
                    // }

                    // For demonstration, remove all matching forms on this page
                    page.Resources.Forms.Remove(form);
                }
            }

            // Save the modified document (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}