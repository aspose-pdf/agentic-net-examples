using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "styled_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Define the custom colors (hex values converted to System.Drawing.Color).
        System.Drawing.Color paragraphTextSysColor   = ColorTranslator.FromHtml("#1A2B3C");
        System.Drawing.Color paragraphBackSysColor   = ColorTranslator.FromHtml("#F0F0F0");
        System.Drawing.Color headerTextSysColor      = System.Drawing.Color.White; // fully qualified to avoid ambiguity
        System.Drawing.Color headerBackSysColor      = ColorTranslator.FromHtml("#006400");
        System.Drawing.Color linkSysColor            = ColorTranslator.FromHtml("#FF0000");

        // Convert System.Drawing.Color to Aspose.Pdf.Color where the PDF API expects it.
        Aspose.Pdf.Color paragraphTextPdfColor = Aspose.Pdf.Color.FromRgb(paragraphTextSysColor);
        Aspose.Pdf.Color paragraphBackPdfColor = Aspose.Pdf.Color.FromRgb(paragraphBackSysColor);
        Aspose.Pdf.Color headerTextPdfColor    = Aspose.Pdf.Color.FromRgb(headerTextSysColor);
        Aspose.Pdf.Color headerBackPdfColor    = Aspose.Pdf.Color.FromRgb(headerBackSysColor);
        Aspose.Pdf.Color linkPdfColor          = Aspose.Pdf.Color.FromRgb(linkSysColor);

        // Apply the colors to the document elements.
        foreach (Page page in doc.Pages)
        {
            // Style text fragments.
            foreach (var paragraph in page.Paragraphs)
            {
                if (paragraph is TextFragment tf)
                {
                    // Simple heuristic: treat fragments that start with "H1:" as a header.
                    if (tf.Text.StartsWith("H1:", StringComparison.OrdinalIgnoreCase))
                    {
                        tf.TextState.ForegroundColor = headerTextPdfColor;
                        tf.TextState.BackgroundColor = headerBackPdfColor;
                    }
                    else
                    {
                        tf.TextState.ForegroundColor = paragraphTextPdfColor;
                        tf.TextState.BackgroundColor = paragraphBackPdfColor;
                    }
                }
            }

            // Style link annotations.
            foreach (var annot in page.Annotations)
            {
                if (annot is LinkAnnotation link)
                {
                    link.Color = linkPdfColor;
                }
            }
        }

        // Save the styled PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
    }
}
