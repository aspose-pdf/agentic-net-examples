using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // <-- added for Annotation types

class AnnotationSummaryExtractor
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string summaryPdfPath = "annotation_summary.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // OPTIONAL: demonstrate use of a Facades class as required
        PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();
        annotationEditor.BindPdf(inputPdfPath);
        // No further Facades calls are needed for this task; we will work with the Document API.

        List<string> summaryLines = new List<string>();

        // Open the source PDF and collect annotation data
        using (Aspose.Pdf.Document srcDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= srcDoc.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = srcDoc.Pages[pageIndex];

                foreach (Annotation annotation in page.Annotations) // <-- Annotation now resolved from Aspose.Pdf.Annotations
                {
                    // Title exists only on markup annotations; cast safely
                    string title = "N/A";
                    if (annotation is MarkupAnnotation markup) // <-- MarkupAnnotation from Aspose.Pdf.Annotations
                    {
                        title = markup.Title;
                    }

                    string contents = annotation.Contents ?? string.Empty;

                    summaryLines.Add($"Page {pageIndex}: Title = \"{title}\", Contents = \"{contents}\"");
                }
            }
        }

        // Create a new PDF that will hold the summary
        using (Aspose.Pdf.Document summaryDoc = new Aspose.Pdf.Document())
        {
            // Add a single page for the summary text
            Aspose.Pdf.Page summaryPage = summaryDoc.Pages.Add();

            // Combine all lines into one block of text
            string allText = string.Join(Environment.NewLine, summaryLines);

            // Create a TextFragment to hold the summary
            TextFragment fragment = new TextFragment(allText)
            {
                // Position the text at the top‑left corner of the page
                Position = new Position(0, summaryPage.PageInfo.Height)
            };

            // Add the fragment to the page
            summaryPage.Paragraphs.Add(fragment);

            // Save the summary PDF
            summaryDoc.Save(summaryPdfPath);
        }

        Console.WriteLine($"Annotation summary saved to '{summaryPdfPath}'.");
    }
}
