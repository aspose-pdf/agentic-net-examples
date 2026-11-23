using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted_links_page10.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based
            const int targetPageNumber = 10;
            if (targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];

            // Prepare a StringWriter to collect extracted text
            using (StringWriter writer = new StringWriter())
            {
                // Iterate over all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Process only LinkAnnotation objects
                    if (annotation is LinkAnnotation link)
                    {
                        // The rectangle defining the clickable area
                        Aspose.Pdf.Rectangle linkRect = link.Rect;

                        // Use TextFragmentAbsorber to extract text within the rectangle
                        TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                        absorber.TextSearchOptions = new TextSearchOptions(linkRect);
                        absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                        // Apply the absorber to the page
                        page.Accept(absorber);

                        // Concatenate all fragments found inside the rectangle
                        string extracted = string.Join(" ", absorber.TextFragments.Select(tf => tf.Text)).Trim();
                        if (!string.IsNullOrEmpty(extracted))
                        {
                            writer.WriteLine($"--- Link Annotation (Rect: {linkRect.LLX},{linkRect.LLY},{linkRect.URX},{linkRect.URY}) ---");
                            writer.WriteLine(extracted);
                            writer.WriteLine();
                        }
                    }
                }

                // Save the collected text to a file
                File.WriteAllText(outputTxtPath, writer.ToString());
                Console.WriteLine($"Extracted text from LinkAnnotations on page {targetPageNumber} saved to '{outputTxtPath}'.");
            }
        }
    }
}
