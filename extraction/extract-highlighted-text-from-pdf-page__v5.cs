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
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based
            const int targetPageNumber = 16;
            if (targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];
            StringBuilder highlightedText = new StringBuilder();

            // Iterate over all annotations on the page
            foreach (Annotation annot in page.Annotations)
            {
                // We are interested only in text markup annotations (e.g., highlights)
                if (annot is TextMarkupAnnotation textMarkup)
                {
                    // Get the fragments of text that are marked by this annotation
                    TextFragmentCollection fragments = textMarkup.GetMarkedTextFragments();

                    foreach (TextFragment fragment in fragments)
                    {
                        highlightedText.Append(fragment.Text);
                    }
                }
            }

            // Write the extracted highlighted text to a plain‑text file
            File.WriteAllText(outputTxtPath, highlightedText.ToString());
            Console.WriteLine($"Highlighted text from page {targetPageNumber} saved to '{outputTxtPath}'.");
        }
    }
}