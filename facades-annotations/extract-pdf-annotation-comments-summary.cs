using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for TextFragment, FontStyles

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string summaryPdfPath = "annotations_summary.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF document.
        Document srcDoc = new Document(inputPdfPath);

        // Collect all annotations from all pages.
        List<Annotation> allAnnotations = new List<Annotation>();
        foreach (Page page in srcDoc.Pages)
        {
            foreach (Annotation ann in page.Annotations)
            {
                allAnnotations.Add(ann);
            }
        }

        // Prepare a new PDF document that will hold the summary.
        using (Document summaryDoc = new Document())
        {
            // Add a single page to the summary document.
            Page summaryPage = summaryDoc.Pages.Add();

            // Add a title for the summary.
            TextFragment title = new TextFragment("Annotation Summary")
            {
                TextState = { FontSize = 18, FontStyle = FontStyles.Bold }
            };
            summaryPage.Paragraphs.Add(title);
            summaryPage.Paragraphs.Add(new TextFragment(Environment.NewLine));

            // Iterate over extracted annotations and write their details.
            foreach (Annotation ann in allAnnotations)
            {
                // Only process text annotations (comments). Adjust as needed for other types.
                if (ann.AnnotationType == AnnotationType.Text && ann is TextAnnotation textAnn)
                {
                    // Build a readable line: Title (if any) and Contents.
                    string line = string.IsNullOrWhiteSpace(textAnn.Title)
                        ? $"Comment: {ann.Contents}"
                        : $"Comment ({textAnn.Title}): {ann.Contents}";

                    TextFragment fragment = new TextFragment(line)
                    {
                        TextState = { FontSize = 12 }
                    };
                    summaryPage.Paragraphs.Add(fragment);
                }
            }

            // Save the summary PDF.
            summaryDoc.Save(summaryPdfPath);
        }

        Console.WriteLine($"Annotation summary saved to '{summaryPdfPath}'.");
    }
}
