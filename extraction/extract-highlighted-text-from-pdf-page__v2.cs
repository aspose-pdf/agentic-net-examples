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

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indices are 1‑based; page 6 is accessed via index 6
            Page page = doc.Pages[6];

            StringBuilder extractedText = new StringBuilder();

            // Iterate over all annotations on the page
            foreach (Annotation annot in page.Annotations)
            {
                // Process only text markup annotations (e.g., highlights)
                if (annot is TextMarkupAnnotation textMarkup)
                {
                    // Retrieve the text fragments that are marked by the annotation
                    var fragments = textMarkup.GetMarkedTextFragments();

                    // Concatenate the text of each fragment
                    foreach (TextFragment fragment in fragments)
                    {
                        extractedText.Append(fragment.Text);
                    }
                }
            }

            // Write the extracted highlighted text to a plain‑text file
            File.WriteAllText(outputPath, extractedText.ToString());
        }

        Console.WriteLine($"Highlighted text from page 6 saved to '{outputPath}'.");
    }
}