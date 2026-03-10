using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampName = "MyStamp"; // name of the stamp to locate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Document disposal follows the recommended using pattern
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (see page-indexing-one-based rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based (annotation-collection-one-based-indexing)
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // We are interested only in StampAnnotation objects
                    if (annotation is StampAnnotation stamp)
                    {
                        // Match the stamp by its Name property
                        if (stamp.Name == stampName)
                        {
                            // Extract the associated text (Contents holds the stamp text)
                            string stampText = stamp.Contents ?? string.Empty;
                            Console.WriteLine($"Stamp '{stampName}' found on page {pageIndex}:");
                            Console.WriteLine(stampText);
                            return; // exit after first match
                        }
                    }
                }
            }

            Console.WriteLine($"Stamp named '{stampName}' was not found in the document.");
        }
    }
}