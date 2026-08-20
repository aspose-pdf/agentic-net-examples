using System;
using System.IO;
using Aspose.Pdf.Facades;

class AnnotationBatchProcessor
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";
        // Folder where exported XFDF files will be archived
        const string archiveFolder = @"C:\XfdfArchive";

        // Ensure output and archive directories exist
        Directory.CreateDirectory(outputFolder);
        Directory.CreateDirectory(archiveFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFilePath);
            string outputPdfPath = Path.Combine(outputFolder, fileName);
            string xfdfFilePath = Path.Combine(archiveFolder,
                Path.GetFileNameWithoutExtension(fileName) + ".xfdf");

            // Use PdfAnnotationEditor facade to work with annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(pdfFilePath);

                // Export all annotations to an XFDF file (archive)
                using (FileStream xfdfStream = File.Create(xfdfFilePath))
                {
                    editor.ExportAnnotationsToXfdf(xfdfStream);
                }

                // Delete all annotations from the PDF
                editor.DeleteAnnotations();

                // Save the modified PDF to the output folder
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Processed '{fileName}': XFDF archived, annotations removed.");
        }

        Console.WriteLine("Batch processing completed.");
    }
}