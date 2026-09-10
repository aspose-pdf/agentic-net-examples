using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationCleanup
{
    // Removes annotations older than the specified number of days from all PDFs in a folder.
    static void Main()
    {
        const string inputFolder  = @"C:\PdfBatch\Input";
        const string outputFolder = @"C:\PdfBatch\Cleaned";
        const int    daysThreshold = 30; // annotations older than 30 days will be removed

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Bind the PDF to the annotation editor facade
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPath);

            // Access the underlying Document to iterate pages and annotations
            Document doc = editor.Document;

            // Calculate the cutoff date
            DateTime cutoff = DateTime.Now.AddDays(-daysThreshold);

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annCollection = page.Annotations;

                // Collect indexes of annotations that are older than the cutoff
                List<int> indexesToDelete = new List<int>();

                // AnnotationCollection uses 1‑based indexing as well
                for (int annIndex = 1; annIndex <= annCollection.Count; annIndex++)
                {
                    Annotation annotation = annCollection[annIndex];

                    // The property that stores the modification date is called 'Modified'.
                    // If the annotation has never been modified, its value may be DateTime.MinValue.
                    DateTime modifiedDate = annotation.Modified;

                    if (modifiedDate != DateTime.MinValue && modifiedDate < cutoff)
                    {
                        indexesToDelete.Add(annIndex);
                    }
                }

                // Delete collected annotations in reverse order to keep indexes valid
                for (int i = indexesToDelete.Count - 1; i >= 0; i--)
                {
                    annCollection.Delete(indexesToDelete[i]);
                }
            }

            // Save the cleaned PDF
            editor.Save(outputPath);
            editor.Close(); // Release resources held by the facade

            Console.WriteLine($"Processed '{fileName}' → '{outputPath}'");
        }

        Console.WriteLine("Annotation cleanup completed.");
    }
}