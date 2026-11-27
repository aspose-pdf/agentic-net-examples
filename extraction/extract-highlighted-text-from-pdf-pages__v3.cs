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

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the requested page range exists.
            int startPage = 12;
            int endPage   = 14;
            if (endPage > doc.Pages.Count) endPage = doc.Pages.Count;

            using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
            {
                // Iterate pages 12‑14 (1‑based indexing).
                for (int pageNum = startPage; pageNum <= endPage; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Examine each annotation on the page.
                    foreach (Annotation annot in page.Annotations)
                    {
                        // Process only text markup annotations (e.g., highlights).
                        if (annot is TextMarkupAnnotation textMarkup)
                        {
                            // Retrieve the text fragments that are marked by the annotation.
                            TextFragmentCollection fragments = textMarkup.GetMarkedTextFragments();

                            // Write each fragment's text to the output file.
                            foreach (TextFragment fragment in fragments)
                            {
                                writer.WriteLine(fragment.Text);
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Highlighted text extracted to '{outputTxtPath}'.");
    }
}