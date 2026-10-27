using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "hyperlinks.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF and open a writer for the output text file
            using (Document doc = new Document(inputPdf))
            using (StreamWriter writer = new StreamWriter(outputTxt, false))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Iterate all annotations on the page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // We are interested only in link annotations
                        if (annotation is LinkAnnotation link)
                        {
                            // The link's action may be a GoToURIAction containing the URL
                            if (link.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                            {
                                writer.WriteLine(uriAction.URI);
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Hyperlinks extracted to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
