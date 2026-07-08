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
        const string outputPdfPath = "annotation_summary.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Extract annotations using PdfAnnotationEditor (Facade API)
        List<Annotation> allAnnotations = new List<Annotation>();
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Determine total page count from the bound document
            int pageCount = editor.Document.Pages.Count;

            // Get all possible annotation types
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Extract annotations from all pages
            IList<Annotation> extracted = editor.ExtractAnnotations(1, pageCount, allTypes);
            if (extracted != null)
                allAnnotations.AddRange(extracted);
        }

        // Compile comments (Contents) into a single summary string
        StringBuilder summaryBuilder = new StringBuilder();
        summaryBuilder.AppendLine("Annotation Summary");
        summaryBuilder.AppendLine("==================");
        summaryBuilder.AppendLine();

        int index = 1;
        foreach (Annotation ann in allAnnotations)
        {
            // Some annotations may not have contents; skip empty entries
            if (!string.IsNullOrWhiteSpace(ann.Contents))
            {
                // The base Annotation class does not expose a PageNumber property.
                // If page information is required, it can be derived from the annotation's location
                // or by iterating pages separately. For this summary we omit the page number.
                summaryBuilder.AppendLine($"[{index}] Comment:");
                summaryBuilder.AppendLine(ann.Contents.Trim());
                summaryBuilder.AppendLine();
                index++;
            }
        }

        // If no annotations were found, note it in the summary
        if (index == 1)
        {
            summaryBuilder.AppendLine("No annotation comments were found in the document.");
        }

        // Create a new PDF document to hold the summary
        using (Document summaryDoc = new Document())
        {
            // Add a blank page
            summaryDoc.Pages.Add();

            // Create a text fragment with the compiled summary
            TextFragment tf = new TextFragment(summaryBuilder.ToString());

            // Set basic text appearance (cross‑platform Aspose.Pdf.Color)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the text fragment to the first page
            summaryDoc.Pages[1].Paragraphs.Add(tf);

            // Save the summary PDF
            summaryDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotation summary saved to '{outputPdfPath}'.");
    }
}
