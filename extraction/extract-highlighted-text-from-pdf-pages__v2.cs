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

        // StringBuilder to collect extracted highlighted text
        StringBuilder extractedText = new StringBuilder();

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page range exists
            int startPage = 8;
            int endPage   = 10;
            int maxPage   = doc.Pages.Count;

            if (startPage > maxPage)
            {
                Console.Error.WriteLine("Document has fewer pages than the start page.");
                return;
            }

            // Clamp endPage to the actual number of pages
            endPage = Math.Min(endPage, maxPage);

            // Iterate pages 8 to 10 (1‑based indexing)
            for (int i = startPage; i <= endPage; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all annotations on the current page
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only highlight annotations
                    if (annot is TextMarkupAnnotation markup &&
                        markup.AnnotationType == AnnotationType.Highlight)
                    {
                        // Get the text fragments that are marked by this highlight
                        TextFragmentCollection fragments = markup.GetMarkedTextFragments();

                        foreach (TextFragment fragment in fragments)
                        {
                            // Append the fragment text followed by a newline
                            extractedText.AppendLine(fragment.Text);
                        }
                    }
                }
            }
        }

        // Write the collected highlighted text to the output file
        try
        {
            File.WriteAllText(outputPath, extractedText.ToString(), Encoding.UTF8);
            Console.WriteLine($"Highlighted text extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write output file: {ex.Message}");
        }
    }
}
