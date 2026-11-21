using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        string sourceFolder = "pdfs";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Folder not found: {sourceFolder}");
            return;
        }

        // Collect all PDF files (including subfolders)
        var pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.AllDirectories);

        // Configure parallel execution (use all logical processors)
        ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        // Process each PDF concurrently
        Parallel.ForEach(pdfFiles, parallelOptions, pdfPath =>
        {
            try
            {
                // Determine the number of pages using Document (ensures proper disposal)
                int pageCount;
                using (Document doc = new Document(pdfPath))
                {
                    pageCount = doc.Pages.Count;
                }

                // Use PdfAnnotationEditor (Aspose.Pdf.Facades) to delete annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);

                    // Delete all annotations on every page. The DeleteAnnotations method in the
                    // version of Aspose.Pdf used here expects the page number as a string, so we
                    // convert the integer page index to string.
                    for (int page = 1; page <= pageCount; page++)
                    {
                        editor.DeleteAnnotations(page.ToString());
                    }

                    // Save changes, overwriting the original file
                    editor.Save(pdfPath);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("All PDFs have been processed and annotations removed.");
    }
}
