using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Look for TextMarkupAnnotation (highlight, underline, etc.)
                    if (annot is TextMarkupAnnotation textMarkup)
                    {
                        // Retrieve the text fragments that lie under the markup (rule: TextMarkupAnnotation.GetMarkedTextFragments)
                        TextFragmentCollection fragments = textMarkup.GetMarkedTextFragments();

                        Console.WriteLine($"Page {i} - Annotation Type: {textMarkup.AnnotationType}");
                        foreach (TextFragment fragment in fragments)
                        {
                            // fragment.Text contains the actual marked text
                            Console.WriteLine($"  Marked Text: \"{fragment.Text}\"");
                        }
                    }
                }
            }
        }
    }
}