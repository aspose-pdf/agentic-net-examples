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
        const string inputPdfPath = "input.pdf";
        const string summaryPdfPath = "annotation_summary.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Extract annotation contents using PdfAnnotationEditor (Facade API)
        string[] extractedContents;
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Pass null for the annotation types array to retrieve all annotation types.
            // Cast null to the specific overload type to avoid ambiguity.
            // ExtractAnnotations returns IList<Annotation>; convert to array for easier handling.
            Annotation[] annotations = editor.ExtractAnnotations(1, int.MaxValue, (AnnotationType[])null).ToArray();

            // Collect the Contents property of each annotation
            extractedContents = new string[annotations.Length];
            for (int i = 0; i < annotations.Length; i++)
            {
                // Some annotations may have null Contents; handle gracefully
                extractedContents[i] = annotations[i].Contents ?? string.Empty;
            }
        }

        // Build a single summary string
        StringBuilder summaryBuilder = new StringBuilder();
        summaryBuilder.AppendLine("Annotation Summary");
        summaryBuilder.AppendLine("==================");
        summaryBuilder.AppendLine();

        for (int i = 0; i < extractedContents.Length; i++)
        {
            string content = extractedContents[i].Trim();
            if (!string.IsNullOrEmpty(content))
            {
                summaryBuilder.AppendLine($"[{i + 1}] {content}");
                summaryBuilder.AppendLine();
            }
        }

        // Create a new PDF document to hold the summary
        using (Document summaryDoc = new Document())
        {
            // Add a blank page
            Page page = summaryDoc.Pages.Add();

            // Create a TextFragment with the compiled summary
            TextFragment fragment = new TextFragment(summaryBuilder.ToString());
            // Set appearance via TextState (font, size, color)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the summary PDF
            summaryDoc.Save(summaryPdfPath);
        }

        Console.WriteLine($"Annotation summary saved to '{summaryPdfPath}'.");
    }
}
