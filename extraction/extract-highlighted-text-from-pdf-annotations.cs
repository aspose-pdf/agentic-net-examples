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
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Accumulate highlighted text fragments
        StringBuilder highlightedText = new StringBuilder();

        // Load PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Process only text markup annotations (e.g., highlights)
                    if (annotation is TextMarkupAnnotation markup)
                    {
                        // Retrieve the text fragments marked by this annotation
                        TextFragmentCollection fragments = markup.GetMarkedTextFragments();

                        foreach (TextFragment fragment in fragments)
                        {
                            highlightedText.Append(fragment.Text);
                            highlightedText.Append(' ');
                        }
                    }
                }
            }
        }

        // Save the collected highlighted text to a plain‑text file
        File.WriteAllText(outputPath, highlightedText.ToString().Trim());

        Console.WriteLine($"Highlighted text saved to '{outputPath}'.");
    }
}