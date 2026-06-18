using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "links.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Prepare the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                // Write CSV header
                writer.WriteLine("URL");

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Iterate through all annotations on the page
                    for (int j = 1; j <= page.Annotations.Count; j++)
                    {
                        Annotation ann = page.Annotations[j];

                        // We are interested only in LinkAnnotation objects
                        if (ann is LinkAnnotation link)
                        {
                            // The correct way to obtain an external URL is via GoToURIAction
                            if (link.Action is GoToURIAction uriAction)
                            {
                                string url = uriAction.URI;
                                // Write the URL to the CSV (escape double quotes if needed)
                                string escaped = url?.Replace("\"", "\"\"");
                                writer.WriteLine($"\"{escaped}\"");
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Link URLs have been written to '{outputCsv}'.");
    }
}