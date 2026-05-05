using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Extract text annotations (comments) from the source PDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            int pageCount = editor.Document.Pages.Count;
            AnnotationType[] types = new AnnotationType[] { AnnotationType.Text };

            // ExtractAnnotations returns IList<Annotation>
            IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, types);

            // Create a new PDF that will contain the summary
            using (Document summaryDoc = new Document())
            {
                // Add a single page for the summary content
                Page summaryPage = summaryDoc.Pages.Add();

                // Starting Y coordinate (top of the page)
                double yPos = summaryPage.PageInfo.Height - 50;

                foreach (Annotation annot in annotations)
                {
                    // Build a line with page number and comment text
                    string line = $"Page {annot.PageIndex}: {annot.Contents}";

                    // Create a text fragment for the line
                    TextFragment tf = new TextFragment(line);
                    tf.Position = new Position(50, yPos);
                    tf.TextState.FontSize = 12;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Add the fragment to the page
                    summaryPage.Paragraphs.Add(tf);

                    // Move down for the next line
                    yPos -= 20;
                }

                // Save the compiled summary PDF
                summaryDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Summary PDF saved to '{outputPath}'.");
    }
}