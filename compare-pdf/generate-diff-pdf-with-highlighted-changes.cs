using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Comparison;

class DiffPdfGenerator
{
    static void Main()
    {
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";
        const string diffPdfPath = "diff_output.pdf";

        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        // Load the two source PDFs
        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            // Default comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page text comparison
            List<List<DiffOperation>> pageDiffs =
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Create a PdfOutputGenerator with the default style (default highlight colors)
            PdfOutputGenerator generator = new PdfOutputGenerator();

            // Generate the diff PDF and save it
            generator.GenerateOutput(pageDiffs, diffPdfPath);
        }

        // Simple verification that highlight colors match the defaults
        // (Deletion = red, Insertion = green in the default style)
        using (Document diffDoc = new Document(diffPdfPath))
        {
            foreach (Page page in diffDoc.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is MarkupAnnotation markup)
                    {
                        Aspose.Pdf.Color color = markup.Color;
                        bool isDefault = color.Equals(Aspose.Pdf.Color.Red) ||
                                         color.Equals(Aspose.Pdf.Color.Green);
                        Console.WriteLine(
                            $"Page {page.Number} annotation color: {color} (default? {isDefault})");
                    }
                }
            }
        }

        Console.WriteLine($"Diff PDF generated at '{diffPdfPath}'.");
    }
}