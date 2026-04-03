using System;
using System.Collections.Generic;
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

            // Create a TextFragmentAbsorber to visit all text fragments on the page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Accept the absorber for the specific page
            page.Accept(absorber);

            // Collect XForm objects that match the criteria
            List<XForm> formsToDelete = new List<XForm>();

            foreach (TextFragment tf in absorber.TextFragments)
            {
                // Each TextFragment may belong to a form XObject
                XForm form = tf.Form;
                if (form != null)
                {
                    // Check that the XObject subtype is "Form"
                    bool isFormSubtype = string.Equals(form.Subtype?.ToString(), "Form", StringComparison.OrdinalIgnoreCase);

                    // Check that the form name indicates a "Typewriter" field (heuristic)
                    bool isTypewriter = form.Name?.IndexOf("Typewriter", StringComparison.OrdinalIgnoreCase) >= 0;

                    if (isFormSubtype && isTypewriter)
                    {
                        formsToDelete.Add(form);
                    }
                }
            }

            // Remove the identified forms from the page's resources
            foreach (XForm f in formsToDelete)
            {
                // XFormCollection.Remove removes the specific XForm instance
                page.Resources.Forms.Remove(f);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}