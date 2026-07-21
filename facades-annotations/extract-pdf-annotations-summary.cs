using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // Added namespace for Annotation class

class AnnotationSummaryExtractor
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF to extract annotations from
        const string summaryPdfPath = "annotations_summary.pdf"; // Output summary PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // StringBuilder to accumulate all annotation comments
        StringBuilder commentsBuilder = new StringBuilder();

        // Use PdfAnnotationEditor (Facades API) to work with annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdfPath);

            // Access the underlying Document
            Document srcDoc = editor.Document;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= srcDoc.Pages.Count; pageIndex++)
            {
                Page page = srcDoc.Pages[pageIndex];

                // Iterate through all annotations on the page
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Only process annotations that have textual content
                    if (!string.IsNullOrEmpty(ann.Contents))
                    {
                        commentsBuilder.AppendLine($"Page {pageIndex}, Annotation {annIndex}:");
                        commentsBuilder.AppendLine(ann.Contents);
                        commentsBuilder.AppendLine(); // blank line between entries
                    }
                }
            }

            // Close the editor (disposes the bound document)
            editor.Close();
        }

        // Create a new PDF that will contain the compiled comments
        using (Document summaryDoc = new Document())
        {
            // Add a single page to hold the summary text
            Page summaryPage = summaryDoc.Pages.Add();

            // Create a TextFragment with the collected comments
            TextFragment summaryFragment = new TextFragment(commentsBuilder.ToString())
            {
                // Optional styling
                TextState = {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };

            // Add the text fragment to the page
            summaryPage.Paragraphs.Add(summaryFragment);

            // Save the summary PDF
            summaryDoc.Save(summaryPdfPath);
        }

        Console.WriteLine($"Annotation summary saved to '{summaryPdfPath}'.");
    }
}
