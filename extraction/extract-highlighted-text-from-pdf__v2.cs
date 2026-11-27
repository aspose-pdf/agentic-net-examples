using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            StringBuilder allHighlightedText = new StringBuilder();

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // We are interested only in TextMarkupAnnotation of type Highlight
                    if (annot is TextMarkupAnnotation textMarkup &&
                        annot.AnnotationType == AnnotationType.Highlight)
                    {
                        // Get the text fragments that are marked by this highlight annotation
                        var fragments = textMarkup.GetMarkedTextFragments();

                        // Concatenate the text of each fragment
                        foreach (var fragment in fragments)
                        {
                            if (!string.IsNullOrEmpty(fragment.Text))
                            {
                                allHighlightedText.Append(fragment.Text);
                                allHighlightedText.Append(' ');
                            }
                        }
                    }
                }
            }

            // Write the concatenated highlighted text to the output file
            File.WriteAllText(outputTxtPath, allHighlightedText.ToString().Trim());
            Console.WriteLine($"Extracted highlighted text saved to '{outputTxtPath}'.");
        }
    }
}
