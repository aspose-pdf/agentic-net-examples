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

        // Ensure the Document is disposed properly
        using (Document doc = new Document(inputPath))
        {
            StringBuilder sb = new StringBuilder();

            // Pages are 1‑based; extract from pages 2 through 4 (if they exist)
            for (int i = 2; i <= 4 && i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only text markup annotations (e.g., highlights)
                    if (annot is TextMarkupAnnotation textMarkup)
                    {
                        // Retrieve the fragments of the highlighted text
                        TextFragmentCollection fragments = textMarkup.GetMarkedTextFragments();

                        foreach (TextFragment fragment in fragments)
                        {
                            sb.AppendLine(fragment.Text);
                        }
                    }
                }
            }

            // Write the collected highlighted text to the output file
            File.WriteAllText(outputPath, sb.ToString());
        }

        Console.WriteLine($"Highlighted text extracted to '{outputPath}'.");
    }
}