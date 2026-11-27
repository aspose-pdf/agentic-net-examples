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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Verify that page 5 exists (pages are 1‑based)
                if (doc.Pages.Count < 5)
                {
                    Console.Error.WriteLine("The document contains fewer than 5 pages.");
                    return;
                }

                Page page = doc.Pages[5];

                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Process only highlight markup annotations
                        if (annotation is TextMarkupAnnotation markup &&
                            annotation.AnnotationType == AnnotationType.Highlight)
                        {
                            // Extract the text fragments marked by the highlight
                            TextFragmentCollection fragments = markup.GetMarkedTextFragments();
                            foreach (TextFragment fragment in fragments)
                            {
                                writer.WriteLine(fragment.Text);
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Highlighted text extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
