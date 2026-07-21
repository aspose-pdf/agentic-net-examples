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
            // Ensure the document has at least three pages (1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                return;
            }

            // Get page three
            Page page3 = doc.Pages[3];

            // Create a TextFragmentAbsorber to collect all text fragments on page three
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            page3.Accept(absorber);

            // Iterate over each TextFragment
            foreach (TextFragment tf in absorber.TextFragments)
            {
                // If the fragment belongs to a form XObject, tf.Form will be non‑null
                XForm form = tf.Form;
                if (form != null && !string.IsNullOrEmpty(form.Name) && form.Name.Contains("Typewriter"))
                {
                    // Remove the matching form from the page's form collection
                    // Delete by name is supported by XFormCollection
                    page3.Resources.Forms.Delete(form.Name);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}