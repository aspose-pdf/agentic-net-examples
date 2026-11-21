using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "annotations.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document and StreamWriter in using blocks for deterministic disposal
        using (Document doc = new Document(inputPath))
        using (StreamWriter writer = new StreamWriter(csvPath))
        {
            // CSV header
            writer.WriteLine("PageNumber,AnnotationType,RectLLX,RectLLY,RectURX,RectURY,Title,Contents,Author,ModifiedDate");

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all annotations on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Basic details common to all annotations
                    string typeName = annotation.GetType().Name;
                    Aspose.Pdf.Rectangle rect = annotation.Rect;

                    // Optional fields – initialise to empty strings
                    string title = string.Empty;
                    string contents = string.Empty;
                    string author = string.Empty;
                    string modDate = string.Empty;

                    // Extract information that is available for the specific annotation type
                    switch (annotation)
                    {
                        case TextAnnotation textAnno:
                            // Title is defined on MarkupAnnotation (base of TextAnnotation)
                            title = textAnno.Title ?? string.Empty;
                            contents = textAnno.Contents ?? string.Empty;
                            // TextAnnotation inherits from MarkupAnnotation – author and modification date are stored there
                            if (textAnno is MarkupAnnotation markupAnno)
                            {
                                author = markupAnno.Subject ?? string.Empty;
                                // Use the correct property for modification date
                                modDate = markupAnno.Modified != DateTime.MinValue ? markupAnno.Modified.ToString("o") : string.Empty;
                            }
                            break;

                        case LinkAnnotation linkAnno:
                            // Capture URI if the link points to a web address (GoToURIAction)
                            if (linkAnno.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                                contents = uriAction.URI;
                            break;

                        // Add other annotation types as needed
                    }

                    // Helper to escape CSV fields containing commas or quotes
                    string Escape(string s) => $"\"{s.Replace("\"", "\"\"")}\"";

                    // Write a CSV line for the current annotation
                    writer.WriteLine($"{i},{Escape(typeName)},{rect.LLX},{rect.LLY},{rect.URX},{rect.URY},{Escape(title)},{Escape(contents)},{Escape(author)},{Escape(modDate)}");
                }
            }
        }

        Console.WriteLine($"Annotations exported to {csvPath}");
    }
}
