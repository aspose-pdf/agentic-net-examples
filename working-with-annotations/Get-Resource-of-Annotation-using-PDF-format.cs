using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Bind the document to the PdfAnnotationEditor facade
                PdfAnnotationEditor editor = new PdfAnnotationEditor();
                editor.BindPdf(doc);

                // Iterate through all pages
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Aspose.Pdf.Page page = doc.Pages[pageNum];

                    // List annotations on the current page
                    if (page.Annotations != null && page.Annotations.Count > 0)
                    {
                        Console.WriteLine($"Page {pageNum} has {page.Annotations.Count} annotation(s):");
                        foreach (Annotation ann in page.Annotations)
                        {
                            Console.WriteLine($"  - Type: {ann.AnnotationType}, Index: {ann.PageIndex}");
                        }
                    }

                    // Access page resources (images, fonts, etc.)
                    Resources resources = page.Resources;
                    if (resources != null && resources.Images != null && resources.Images.Count > 0)
                    {
                        Console.WriteLine($"  Resources (Images) on page {pageNum}:");
                        foreach (XImage img in resources.Images)
                        {
                            // Image name may be empty; use its dimensions for identification
                            string nameInfo = string.IsNullOrEmpty(img.Name) ? "(unnamed)" : img.Name;
                            Console.WriteLine($"    * Image {nameInfo}: {img.Width}x{img.Height}");
                        }
                    }
                }

                // No modifications are made; just save a copy if needed
                string outputPath = "output_copy.pdf";
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}