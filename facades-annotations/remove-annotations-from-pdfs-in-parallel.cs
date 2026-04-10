using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFolder = @"C:\Pdf\Input";
        const string outputFolder = @"C:\Pdf\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: '{inputFolder}'.");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.AllDirectories);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                // Each iteration gets its own PdfAnnotationEditor instance.
                // Using a using‑statement guarantees proper disposal (Close is called internally).
                using (var editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);
                    editor.DeleteAnnotations();
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Annotation removal completed.");
    }
}
