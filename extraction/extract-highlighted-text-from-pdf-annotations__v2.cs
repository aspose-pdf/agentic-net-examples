using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Page indexing in Aspose.Pdf is 1‑based; page 2 is accessed via Pages[2]
                Page page = doc.Pages[2];

                // StringBuilder to accumulate highlighted text
                System.Text.StringBuilder highlightedText = new System.Text.StringBuilder();

                // Iterate over all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Process only TextMarkupAnnotation (e.g., Highlight)
                    if (annotation is TextMarkupAnnotation markup)
                    {
                        // Get the text fragments that are marked by this annotation
                        TextFragmentCollection fragments = markup.GetMarkedTextFragments();

                        // Concatenate the text of each fragment
                        foreach (TextFragment fragment in fragments)
                        {
                            highlightedText.AppendLine(fragment.Text);
                        }
                    }
                }

                // Write the collected highlighted text to the output file
                File.WriteAllText(outputTxt, highlightedText.ToString());

                Console.WriteLine($"Extracted highlighted text saved to '{outputTxt}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}