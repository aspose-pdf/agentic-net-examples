using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputDirectory = "input-pdfs";
        string outputDirectory = "output-pdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        ConcurrentBag<string> errors = new ConcurrentBag<string>();

        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                string fileName = Path.GetFileName(pdfPath);
                string outputPath = Path.Combine(outputDirectory, fileName);

                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);
                    editor.DeleteAnnotations();
                    editor.Save(outputPath);
                }
            }
            catch (Exception ex)
            {
                errors.Add($"{pdfPath}: {ex.Message}");
            }
        });

        if (!errors.IsEmpty)
        {
            Console.Error.WriteLine("Some files could not be processed:");
            foreach (string err in errors)
            {
                Console.Error.WriteLine(err);
            }
        }
        else
        {
            Console.WriteLine("All PDFs processed successfully.");
        }
    }
}
