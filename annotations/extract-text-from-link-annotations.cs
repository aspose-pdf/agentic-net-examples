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
        const string outputPath = "extracted_links.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        StringBuilder sb = new StringBuilder();

        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is LinkAnnotation link)
                    {
                        // Rectangle of the link annotation (fully qualified to avoid ambiguity)
                        Aspose.Pdf.Rectangle rect = link.Rect;

                        // TextAbsorber limited to the annotation rectangle
                        TextAbsorber absorber = new TextAbsorber();
                        // TextSearchOptions now requires a bool argument (e.g., search in annotations)
                        absorber.TextSearchOptions = new TextSearchOptions(true)
                        {
                            Rectangle = rect
                        };

                        // Extract text from the page region
                        page.Accept(absorber);
                        string extracted = absorber.Text?.Trim();

                        if (!string.IsNullOrEmpty(extracted))
                        {
                            sb.AppendLine($"Page {i}, Link at [{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}]:");
                            sb.AppendLine(extracted);
                            sb.AppendLine(); // separator
                        }
                    }
                }
            }
        }

        // Save all extracted texts to a .txt file
        File.WriteAllText(outputPath, sb.ToString());
        Console.WriteLine($"Extracted link texts saved to '{outputPath}'.");
    }
}
