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

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document has fewer than 9 pages.");
                return;
            }

            Page page = doc.Pages[9];
            StringBuilder highlightedTexts = new StringBuilder();

            foreach (Annotation annotation in page.Annotations)
            {
                // Use Annotation.AnnotationType to identify the annotation kind.
                if (annotation is TextMarkupAnnotation markup &&
                    annotation.AnnotationType == AnnotationType.Highlight)
                {
                    // Retrieve the text fragments covered by the highlight.
                    TextFragmentCollection fragments = markup.GetMarkedTextFragments();

                    foreach (TextFragment fragment in fragments)
                    {
                        highlightedTexts.AppendLine(fragment.Text);
                    }
                }
            }

            File.WriteAllText(outputPath, highlightedTexts.ToString());
        }

        Console.WriteLine($"Highlighted text extracted to '{outputPath}'.");
    }
}
