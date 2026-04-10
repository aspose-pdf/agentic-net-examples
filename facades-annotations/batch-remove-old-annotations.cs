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
        const string outputFolder = @"C:\PdfBatch\Output";
        const int    daysThreshold = 30; // annotations older than 30 days will be removed

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Bind the PDF to the facade
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);
                    Document doc = editor.Document; // underlying Aspose.Pdf.Document

                    // Iterate through all pages (1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];
                        AnnotationCollection annots = page.Annotations;

                        // Collect indexes of annotations that are older than the threshold
                        List<int> indexesToDelete = new List<int>();
                        for (int i = 1; i <= annots.Count; i++)
                        {
                            Annotation annot = annots[i];
                            // Annotation.Modified returns a non‑nullable DateTime.
                            // Compare it directly with the current date.
                            DateTime modified = annot.Modified;
                            if ((DateTime.Now - modified).TotalDays > daysThreshold)
                            {
                                indexesToDelete.Add(i);
                            }
                        }

                        // Delete collected annotations in reverse order to keep indexes valid
                        for (int i = indexesToDelete.Count - 1; i >= 0; i--)
                        {
                            annots.Delete(indexesToDelete[i]);
                        }
                    }

                    // Save the cleaned PDF
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Annotation cleanup completed.");
    }
}
