using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "hyperlinks.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        List<string> hyperlinkList = new List<string>();

        using (Document doc = new Document(inputPath))
        {
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];
                    LinkAnnotation link = annotation as LinkAnnotation;
                    if (link != null && link.Action is GoToURIAction)
                    {
                        GoToURIAction uriAction = (GoToURIAction)link.Action;
                        if (!string.IsNullOrEmpty(uriAction.URI))
                        {
                            hyperlinkList.Add(uriAction.URI);
                        }
                    }
                }
            }
        }

        File.WriteAllLines(outputPath, hyperlinkList);
        Console.WriteLine($"Extracted {hyperlinkList.Count} hyperlink(s) to '{outputPath}'.");
    }
}