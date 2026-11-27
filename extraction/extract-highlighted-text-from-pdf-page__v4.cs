using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";
        const int targetPageNumber = 7; // 1‑based page index

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Ensure the requested page exists
                if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                    return;
                }

                // Get the specified page
                Page page = doc.Pages[targetPageNumber];

                // Open the output file for writing
                using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
                {
                    // Iterate over all annotations on the page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Process only text markup annotations (e.g., highlights)
                        if (annotation is TextMarkupAnnotation markup)
                        {
                            // Retrieve the text fragments marked by this annotation
                            var fragments = markup.GetMarkedTextFragments();

                            // Write each fragment's text to the output file
                            foreach (var fragment in fragments)
                            {
                                writer.WriteLine(fragment.Text);
                            }
                        }
                    }
                }

                Console.WriteLine($"Highlighted text extracted to '{outputTxtPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}