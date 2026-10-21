using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expect one or more PDF file paths as command‑line arguments.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: AnnotationSummary <pdf1> [pdf2 ...]");
            return;
        }

        foreach (string pdfPath in args)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            try
            {
                // Load the PDF document. Aspose.Pdf.Document provides access to page annotations.
                Document pdfDoc = new Document(pdfPath);

                // Aggregate counts per annotation type.
                Dictionary<string, int> typeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                foreach (Page page in pdfDoc.Pages)
                {
                    foreach (Annotation ann in page.Annotations)
                    {
                        // AnnotationType is an enum; use its name for a readable key.
                        string typeName = ann.AnnotationType.ToString();
                        if (typeCounts.ContainsKey(typeName))
                            typeCounts[typeName]++;
                        else
                            typeCounts[typeName] = 1;
                    }
                }

                // Output the summary for the current PDF.
                Console.WriteLine($"Report for: {pdfPath}");
                if (typeCounts.Count == 0)
                {
                    Console.WriteLine("  No annotations found.");
                }
                else
                {
                    foreach (var kvp in typeCounts.OrderBy(k => k.Key))
                    {
                        Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {pdfPath}: {ex.Message}");
            }
        }
    }
}
