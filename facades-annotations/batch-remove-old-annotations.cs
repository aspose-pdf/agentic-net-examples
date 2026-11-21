using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // required by task specification

class Program
{
    // Removes annotations older than the specified number of days from all PDFs in a folder.
    static void Main()
    {
        const string inputFolder  = @"C:\PdfBatch\Input";
        const string outputFolder = @"C:\PdfBatch\Output";
        const int    daysThreshold = 30; // annotations older than 30 days will be removed

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Compute the cutoff date once.
        DateTime cutoffDate = DateTime.Now.AddDays(-daysThreshold);

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outPath  = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF inside a using block for deterministic disposal.
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate through all pages (1‑based indexing).
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];
                        AnnotationCollection annCollection = page.Annotations;

                        // Iterate backwards so that removal does not affect the index order.
                        for (int i = annCollection.Count - 1; i >= 0; i--)
                        {
                            // All annotation types derive from Aspose.Pdf.Annotations.Annotation.
                            Annotation ann = annCollection[i] as Annotation;
                            if (ann == null)
                                continue;

                            // Only markup annotations expose a modification date.
                            if (ann is MarkupAnnotation markup)
                            {
                                // The Modified property holds the last modification date.
                                DateTime modDate = markup.Modified;
                                if (modDate < cutoffDate)
                                {
                                    // Delete the annotation from the collection.
                                    annCollection.Delete(i);
                                }
                            }
                            // Non‑markup annotations are left untouched (or could be removed based on other criteria).
                        }
                    }

                    // Save the cleaned PDF to the output folder.
                    doc.Save(outPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch cleanup completed.");
    }
}
