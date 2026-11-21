using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "links.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        var links = new List<string>();

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    if (page.Annotations == null) continue;

                    // Iterate over all annotations on the page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Look for LinkAnnotation with a GoToURIAction
                        if (annotation is LinkAnnotation linkAnno && linkAnno.Action is GoToURIAction uriAction)
                        {
                            string uri = uriAction.URI;
                            if (!string.IsNullOrEmpty(uri))
                                links.Add(uri);
                        }
                    }
                }
            }

            // Export the collected hyperlinks to a plain‑text file
            File.WriteAllLines(outputTxt, links);
            Console.WriteLine($"Extracted {links.Count} hyperlink(s) to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
