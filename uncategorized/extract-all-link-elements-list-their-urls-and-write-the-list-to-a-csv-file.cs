using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "links.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        using (var writer = new StreamWriter(csvPath))
        {
            writer.WriteLine("Page,LinkURL");

            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];
                    if (ann is LinkAnnotation link)
                    {
                        string url = null;
                        // The URL is stored in the GoToURIAction attached to the link annotation.
                        if (link.Action is GoToURIAction goTo && !string.IsNullOrEmpty(goTo.URI))
                        {
                            url = goTo.URI;
                        }

                        if (!string.IsNullOrEmpty(url))
                        {
                            // Escape double quotes for CSV compliance.
                            string escaped = url.Replace("\"", "\"\"");
                            writer.WriteLine($"{pageIndex},\"{escaped}\"");
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Link URLs have been written to '{csvPath}'.");
    }
}
