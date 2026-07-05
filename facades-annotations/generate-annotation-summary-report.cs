using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Example usage: pass PDF file paths as command‑line arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide at least one PDF file path.");
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
                // Use PdfAnnotationEditor (a Facade) to work with annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);                     // Load the PDF
                    Document doc = editor.Document;              // Access underlying Document

                    // Count annotations by their type
                    var counts = new Dictionary<AnnotationType, int>();

                    // Pages are 1‑based in Aspose.Pdf
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];
                        foreach (Annotation ann in page.Annotations)
                        {
                            AnnotationType type = ann.AnnotationType;
                            if (counts.ContainsKey(type))
                                counts[type]++;
                            else
                                counts[type] = 1;
                        }
                    }

                    // Output summary for this PDF
                    Console.WriteLine($"--- Annotation summary for \"{Path.GetFileName(pdfPath)}\" ---");
                    if (counts.Count == 0)
                    {
                        Console.WriteLine("No annotations found.");
                    }
                    else
                    {
                        foreach (var kvp in counts)
                        {
                            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing \"{pdfPath}\": {ex.Message}");
            }
        }
    }
}