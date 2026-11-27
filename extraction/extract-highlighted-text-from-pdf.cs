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
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine the last page to process (pages are 1‑based)
            int lastPage = Math.Min(5, doc.Pages.Count);

            // Open the output text file for writing
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Iterate over pages 1 through lastPage
                for (int i = 1; i <= lastPage; i++)
                {
                    Page page = doc.Pages[i];

                    // Examine each annotation on the current page
                    foreach (Annotation annot in page.Annotations)
                    {
                        // Process only highlight markup annotations
                        if (annot is HighlightAnnotation highlight)
                        {
                            // Retrieve the text fragments marked by the annotation
                            TextFragmentCollection fragments = highlight.GetMarkedTextFragments();

                            // Write each fragment's text to the output file
                            foreach (TextFragment fragment in fragments)
                            {
                                writer.WriteLine(fragment.Text);
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Highlighted text extracted to '{outputPath}'.");
    }
}
