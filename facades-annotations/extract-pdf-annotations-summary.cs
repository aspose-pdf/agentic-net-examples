using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added for TextFragment, FontRepository, MarginInfo

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

        // Extract annotations to XFDF (XML) using PdfAnnotationEditor (Facades API)
        XDocument xfdfDoc;
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            using (MemoryStream xfdfStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0;
                xfdfDoc = XDocument.Load(xfdfStream);
            }

            editor.Close(); // optional, Dispose will be called by using
        }

        // Build a textual summary from the XFDF contents
        StringBuilder summaryBuilder = new StringBuilder();
        summaryBuilder.AppendLine("Annotations Summary");
        summaryBuilder.AppendLine("====================");
        summaryBuilder.AppendLine();

        // XFDF structure: <xfdf><annots><annot ...><contents>...</contents></annot>...</annots></xfdf>
        foreach (var annot in xfdfDoc.Descendants("annot"))
        {
            string page = annot.Attribute("page")?.Value ?? "N/A";
            string contents = annot.Element("contents")?.Value?.Trim();

            if (!string.IsNullOrEmpty(contents))
            {
                summaryBuilder.AppendLine($"Page: {page}");
                summaryBuilder.AppendLine(contents);
                summaryBuilder.AppendLine(); // blank line between annotations
            }
        }

        // Create a new PDF document to hold the summary
        using (Document summaryDoc = new Document())
        {
            // Add a blank page
            summaryDoc.Pages.Add();

            // Add the summary text as a TextFragment
            TextFragment tf = new TextFragment(summaryBuilder.ToString())
            {
                // Optional styling
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") },
                // Ensure the text fits within the page margins
                Margin = { Top = 20, Bottom = 20, Left = 20, Right = 20 }
            };

            summaryDoc.Pages[1].Paragraphs.Add(tf);

            // Save the summary PDF
            summaryDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotation summary saved to '{outputPdfPath}'.");
    }
}
