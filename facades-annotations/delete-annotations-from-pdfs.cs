using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: AsposePdfDeleteAnnotations <SourceFolder> [OutputFolder]");
            return;
        }

        string sourceFolder = args[0];
        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(sourceFolder, "Cleaned");

        if (!Directory.Exists(sourceFolder))
        {
            Console.WriteLine($"Source folder does not exist: {sourceFolder}");
            return;
        }

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        foreach (string pdfPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_clean.pdf");

                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);
                    editor.DeleteAnnotations();
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
            }
        }
    }
}
