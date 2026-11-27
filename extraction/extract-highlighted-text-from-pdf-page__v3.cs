using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Page indices are 1‑based; page 11 is accessed via Pages[11]
                if (doc.Pages.Count < 11)
                {
                    Console.Error.WriteLine("The document has fewer than 11 pages.");
                    return;
                }

                Page page = doc.Pages[11];
                // Accumulate highlighted text fragments
                var highlightedText = string.Empty;

                foreach (Annotation annot in page.Annotations)
                {
                    // Look for text markup annotations (e.g., highlight)
                    if (annot is TextMarkupAnnotation textMarkup)
                    {
                        // Get the fragments of text that are marked by this annotation
                        TextFragmentCollection fragments = textMarkup.GetMarkedTextFragments();

                        foreach (TextFragment fragment in fragments)
                        {
                            highlightedText += fragment.Text;
                        }
                    }
                }

                // Write the extracted text to a plain‑text file
                File.WriteAllText(outputTxtPath, highlightedText);
                Console.WriteLine($"Highlighted text from page 11 saved to '{outputTxtPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}