using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // Added for TextFragment, FontStyles, and Color

class AnnotationReporter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string reportPdfPath = "annotation_report.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Dictionary to hold annotations grouped by their type
            var groups = new Dictionary<AnnotationType, List<Annotation>>();

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                Page page = srcDoc.Pages[i];
                AnnotationCollection annColl = page.Annotations;

                // Iterate through annotations on the current page
                foreach (Annotation ann in annColl)
                {
                    AnnotationType type = ann.AnnotationType;

                    if (!groups.ContainsKey(type))
                        groups[type] = new List<Annotation>();

                    groups[type].Add(ann);
                }
            }

            // Create a new PDF document to hold the report
            using (Document reportDoc = new Document())
            {
                // Add a page for the report
                Page reportPage = reportDoc.Pages.Add();

                // Title paragraph
                TextFragment title = new TextFragment("Annotation Report")
                {
                    TextState = { FontSize = 18, FontStyle = FontStyles.Bold, ForegroundColor = Color.Blue }
                };
                reportPage.Paragraphs.Add(title);
                reportPage.Paragraphs.Add(new TextFragment("\n"));

                // Generate report content
                foreach (var kvp in groups)
                {
                    AnnotationType type = kvp.Key;
                    int count = kvp.Value.Count;

                    TextFragment line = new TextFragment($"{type}: {count} annotation(s)");
                    line.TextState.FontSize = 12;
                    line.TextState.ForegroundColor = Color.Black;
                    reportPage.Paragraphs.Add(line);
                }

                // If there were no annotations, note that
                if (groups.Count == 0)
                {
                    TextFragment none = new TextFragment("No annotations found in the document.");
                    none.TextState.FontSize = 12;
                    none.TextState.ForegroundColor = Color.Gray;
                    reportPage.Paragraphs.Add(none);
                }

                // Save the report PDF
                reportDoc.Save(reportPdfPath);
            }

            Console.WriteLine($"Annotation report saved to '{reportPdfPath}'.");
        }
    }
}
