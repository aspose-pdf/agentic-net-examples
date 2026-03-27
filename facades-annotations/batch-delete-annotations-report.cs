using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        string inputFolder = "input_pdfs";
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the specified folder.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                int totalAnnotations = 0;
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    totalAnnotations += doc.Pages[pageIndex].Annotations.Count;
                }

                // Prepare output file name (simple filename, no directory)
                string outputFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_clean.pdf";

                // Delete all annotations using PdfAnnotationEditor
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);
                    editor.DeleteAnnotations();
                    editor.Save(outputFileName);
                }

                Console.WriteLine($"{Path.GetFileName(pdfPath)}: {totalAnnotations} annotations removed, saved as {outputFileName}");
            }
        }
    }
}