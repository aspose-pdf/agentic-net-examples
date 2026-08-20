using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class AnnotationSummaryExtractor
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // PDF with annotations
        const string outputPdfPath  = "annotation_summary.pdf"; // Summary PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the source PDF and extract annotation comments
        // -----------------------------------------------------------------
        string[] commentTexts;

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the editor to the source PDF
            editor.BindPdf(inputPdfPath);

            // Get the underlying Document to know page count
            Document sourceDoc = editor.Document;
            int pageCount = sourceDoc.Pages.Count;

            // Extract all text‑type annotations (e.g., sticky notes, free‑text)
            // AnnotationType.Text covers typical comment annotations.
            IList<Annotation> annotations = editor.ExtractAnnotations(
                1,                                   // start page (1‑based)
                pageCount,                           // end page
                new AnnotationType[] { AnnotationType.Text });

            // Collect the Contents of each annotation (the comment text)
            commentTexts = new string[annotations.Count];
            for (int i = 0; i < annotations.Count; i++)
            {
                // Some annotation types expose the comment via the Contents property
                commentTexts[i] = annotations[i].Contents ?? string.Empty;
            }

            // Close the editor (optional, as using will dispose it)
            editor.Close();
        }

        // -----------------------------------------------------------------
        // 2. Build a single summary string
        // -----------------------------------------------------------------
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Annotation Comments Summary");
        sb.AppendLine("==========================");
        sb.AppendLine();

        for (int i = 0; i < commentTexts.Length; i++)
        {
            sb.AppendLine($"Comment {i + 1}:");
            sb.AppendLine(commentTexts[i]);
            sb.AppendLine();
        }

        string summaryText = sb.ToString();

        // -----------------------------------------------------------------
        // 3. Create a new PDF document and write the summary into it
        // -----------------------------------------------------------------
        using (Document summaryDoc = new Document())
        {
            // Add a blank page (Pages.Add creates a new page)
            summaryDoc.Pages.Add();

            // Create a TextFragment with the compiled summary
            TextFragment fragment = new TextFragment(summaryText)
            {
                // Optional styling
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };

            // Add the fragment to the first page
            summaryDoc.Pages[1].Paragraphs.Add(fragment);

            // Save the summary PDF
            summaryDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotation summary saved to '{outputPdfPath}'.");
    }
}
