using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            StringBuilder allHighlightedText = new StringBuilder();

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // We are interested only in text markup annotations (e.g., Highlight)
                    if (annot is TextMarkupAnnotation textMarkup)
                    {
                        // Get the fragments of text that were marked by the annotation
                        TextFragmentCollection fragments = textMarkup.GetMarkedTextFragments();

                        // Concatenate the text of each fragment
                        foreach (TextFragment fragment in fragments)
                        {
                            if (!string.IsNullOrEmpty(fragment.Text))
                            {
                                allHighlightedText.Append(fragment.Text);
                                allHighlightedText.Append(' '); // optional separator
                            }
                        }
                    }
                }
            }

            // Write the concatenated highlighted text to a plain text file
            File.WriteAllText(outputPath, allHighlightedText.ToString().Trim());
        }

        Console.WriteLine($"Highlighted text extracted to '{outputPath}'.");
    }
}