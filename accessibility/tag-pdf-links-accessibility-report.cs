using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";
        const string reportPath = "tag_report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        StringBuilder report = new StringBuilder();

        using (Document doc = new Document(inputPath))
        {
            // Access (or create) tagged content
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Iterate pages and find link annotations
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is LinkAnnotation linkAnnot)
                    {
                        // Retrieve the URI from a GoToURIAction (newer Aspose.PDF versions)
                        string uri = "(no URI)";
                        if (linkAnnot.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                        {
                            uri = uriAction.URI;
                        }

                        // Create a LinkElement in the structure tree
                        LinkElement linkElem = tagged.CreateLinkElement();

                        // Set displayed text (URI) and alternative text
                        linkElem.SetText(uri);
                        linkElem.AlternativeText = $"Link on page {i}";

                        // Attach to the root
                        root.AppendChild(linkElem);

                        // Record the modification
                        report.AppendLine($"Page {i}: Tagged link URI = '{uri}'");
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        // Write the modification report
        File.WriteAllText(reportPath, report.ToString());
        Console.WriteLine($"Tagging completed. PDF saved to '{outputPath}'. Report saved to '{reportPath}'.");
    }
}
