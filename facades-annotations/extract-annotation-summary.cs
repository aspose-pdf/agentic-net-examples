using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Collect annotation comments
        StringBuilder sb = new StringBuilder();

        using (Document sourceDoc = new Document(inputPath))
        {
            for (int pageIndex = 1; pageIndex <= sourceDoc.Pages.Count; pageIndex++)
            {
                Page page = sourceDoc.Pages[pageIndex];
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];
                    if (!string.IsNullOrEmpty(annotation.Contents))
                    {
                        sb.AppendLine($"Page {pageIndex}, Annotation {annIndex}: {annotation.Contents}");
                    }
                }
            }
        }

        // Create summary PDF
        using (Document summaryDoc = new Document())
        {
            Page summaryPage = summaryDoc.Pages.Add();
            TextFragment tf = new TextFragment(sb.ToString());
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            summaryPage.Paragraphs.Add(tf);
            summaryDoc.Save(outputPath);
        }

        Console.WriteLine($"Summary PDF saved to '{outputPath}'.");
    }
}