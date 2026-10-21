using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains the source annotations
        const string templatePath = "template.pdf";

        // List of PDFs that should receive the copied annotations
        string[] targetPaths = { "target1.pdf", "target2.pdf", "target3.pdf" };

        // Directory where the annotated PDFs will be saved
        const string outputDirectory = "Output";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // ------------------------------------------------------------
        // Load template document and cache its annotations per page
        // ------------------------------------------------------------
        Document templateDoc = new Document(templatePath);
        var templateAnnotations = new List<List<Annotation>>();
        foreach (Page page in templateDoc.Pages)
        {
            var pageAnnotations = new List<Annotation>();
            foreach (Annotation ann in page.Annotations)
            {
                // Clone each annotation so it can be reused for other documents
                pageAnnotations.Add((Annotation)ann.Clone());
            }
            templateAnnotations.Add(pageAnnotations);
        }

        // ------------------------------------------------------------
        // Import the cached annotations into each target PDF
        // ------------------------------------------------------------
        foreach (string targetPath in targetPaths)
        {
            if (!File.Exists(targetPath))
            {
                Console.Error.WriteLine($"Target PDF not found: {targetPath}");
                continue;
            }

            string outputPath = Path.Combine(outputDirectory, Path.GetFileName(targetPath));

            using (Document targetDoc = new Document(targetPath))
            {
                // Ensure the target has at least as many pages as the template
                while (targetDoc.Pages.Count < templateDoc.Pages.Count)
                {
                    targetDoc.Pages.Add();
                }

                // Copy annotations page‑by‑page
                for (int i = 0; i < templateDoc.Pages.Count; i++)
                {
                    Page targetPage = targetDoc.Pages[i + 1];
                    foreach (Annotation ann in templateAnnotations[i])
                    {
                        // Clone again because the same instance cannot be added to multiple pages
                        targetPage.Annotations.Add((Annotation)ann.Clone());
                    }
                }

                // Save the modified document
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Annotations copied to: {outputPath}");
        }
    }
}
