using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF that will receive the annotations
        const string pdfPath = "input.pdf";

        // List of FDF files containing annotations to import
        string[] fdfFiles = { "annotations1.fdf", "annotations2.fdf", "annotations3.fdf" };

        // Output PDF with all imported annotations
        const string outputPath = "consolidated_output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Verify each FDF file exists before processing
        foreach (var fdf in fdfFiles)
        {
            if (!File.Exists(fdf))
            {
                Console.Error.WriteLine($"FDF not found: {fdf}");
                return;
            }
        }

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Import annotations from each FDF file into the same document
                foreach (var fdfPath in fdfFiles)
                {
                    using (FileStream fdfStream = File.OpenRead(fdfPath))
                    {
                        // Static method reads annotations and adds them to the document
                        FdfReader.ReadAnnotations(fdfStream, doc);
                    }
                }

                // Consolidate all annotations from all pages into a single collection (List)
                List<Annotation> allAnnotations = new List<Annotation>();
                foreach (Page page in doc.Pages)
                {
                    // AnnotationCollection implements IEnumerable<Annotation>
                    foreach (Annotation ann in page.Annotations)
                    {
                        allAnnotations.Add(ann);
                    }
                }

                // Example usage of the consolidated collection:
                Console.WriteLine($"Total annotations imported: {allAnnotations.Count}");

                // Save the PDF with the merged annotations
                doc.Save(outputPath);
            }

            Console.WriteLine($"Consolidated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}