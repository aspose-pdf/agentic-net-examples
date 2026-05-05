using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationSummaryUtility
{
    static void Main(string[] args)
    {
        // Expect a directory path containing PDF files as the first argument.
        if (args.Length == 0 || !Directory.Exists(args[0]))
        {
            Console.Error.WriteLine("Please provide a valid directory path containing PDF files.");
            return;
        }

        string inputDirectory = args[0];
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the specified directory.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF to obtain page count (required for extraction range).
                using (Document doc = new Document(pdfPath))
                {
                    int pageCount = doc.Pages.Count;

                    // Use PdfAnnotationEditor (Facade) to work with annotations.
                    PdfAnnotationEditor editor = new PdfAnnotationEditor();
                    editor.BindPdf(pdfPath);

                    // Extract all annotations in the document.
                    // Passing an empty AnnotationType array returns all annotation types.
                    IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, new AnnotationType[0]);

                    // Count annotations by their type.
                    Dictionary<string, int> typeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    foreach (Annotation ann in annotations)
                    {
                        string typeName = ann.AnnotationType.ToString();
                        if (typeCounts.ContainsKey(typeName))
                            typeCounts[typeName]++;
                        else
                            typeCounts[typeName] = 1;
                    }

                    // Output the summary for the current PDF.
                    Console.WriteLine($"--- Annotation Summary for: {Path.GetFileName(pdfPath)} ---");
                    if (typeCounts.Count == 0)
                    {
                        Console.WriteLine("No annotations found.");
                    }
                    else
                    {
                        foreach (var kvp in typeCounts)
                        {
                            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                        }
                    }
                    Console.WriteLine();

                    // Clean up the facade.
                    editor.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}