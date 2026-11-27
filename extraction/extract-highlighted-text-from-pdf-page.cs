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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            const int targetPageNumber = 4;
            if (targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];
            StringBuilder extractedText = new StringBuilder();

            // Iterate over all annotations on the page
            foreach (Annotation annotation in page.Annotations)
            {
                // We are interested only in TextMarkupAnnotation (e.g., highlight)
                if (annotation is TextMarkupAnnotation textMarkup)
                {
                    // Get the text fragments that are marked by this annotation
                    TextFragmentCollection fragments = textMarkup.GetMarkedTextFragments();

                    // Concatenate the text of each fragment
                    foreach (TextFragment fragment in fragments)
                    {
                        extractedText.Append(fragment.Text);
                    }

                    // If multiple highlight annotations exist, separate their texts
                    extractedText.AppendLine();
                }
            }

            // Write the collected text to a plain‑text file
            File.WriteAllText(outputPath, extractedText.ToString());
        }

        Console.WriteLine($"Highlighted text from page {4} saved to '{outputPath}'.");
    }
}