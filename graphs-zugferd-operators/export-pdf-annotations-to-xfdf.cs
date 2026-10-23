using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "annotations.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Prepare a simple FDF file (header and trailer)
            using (StreamWriter writer = new StreamWriter(outputFdfPath, false))
            {
                writer.WriteLine("%FDF-1.2");
                writer.WriteLine("%âãÏÓ"); // Binary comment as per FDF spec
                writer.WriteLine("1 0 obj");
                writer.WriteLine("<<");
                writer.WriteLine("/FDF");
                writer.WriteLine("<<");
                writer.WriteLine("/Fields [");

                // Iterate through all pages and their annotations
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];
                    foreach (Annotation annot in page.Annotations)
                    {
                        writer.WriteLine("<<");
                        // Use AnnotationType instead of the non‑existent Subtype property
                        writer.WriteLine($"/Subtype /{annot.AnnotationType}");
                        writer.WriteLine($"/Rect [{annot.Rect.LLX} {annot.Rect.LLY} {annot.Rect.URX} {annot.Rect.URY}]");

                        // Title is only available on markup annotations
                        if (annot is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                        {
                            writer.WriteLine($"/T ({EscapeFdfString(markup.Title)})");
                        }

                        // Contents is available on the base Annotation class
                        if (!string.IsNullOrEmpty(annot.Contents))
                        {
                            writer.WriteLine($"/V ({EscapeFdfString(annot.Contents)})");
                        }

                        writer.WriteLine(">>");
                    }
                }

                writer.WriteLine("]"); // End of /Fields array
                writer.WriteLine(">>"); // End of /FDF dictionary
                writer.WriteLine(">>"); // End of root dictionary
                writer.WriteLine("endobj");
                writer.WriteLine("trailer");
                writer.WriteLine("<< /Root 1 0 R >>");
                writer.WriteLine("%%EOF");
            }
        }

        Console.WriteLine($"Annotations exported to FDF file: {outputFdfPath}");
    }

    // Helper to escape parentheses and backslashes in FDF strings
    private static string EscapeFdfString(string value)
    {
        return value.Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
    }
}
