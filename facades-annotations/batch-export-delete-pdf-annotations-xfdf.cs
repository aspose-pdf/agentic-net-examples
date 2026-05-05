using System;
using System.IO;
using Aspose.Pdf.Facades;

class AnnotationBatchProcessor
{
    static void Main()
    {
        // Input folder containing PDF files to process
        const string inputFolder = "InputPdfs";
        // Folder where exported XFDF files will be archived
        const string archiveFolder = "ArchivedXfdf";

        // Ensure the archive folder exists
        Directory.CreateDirectory(archiveFolder);

        // Ensure the input folder exists – if it does not, create it and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder '{inputFolder}' was not found and has been created.\nPlace PDF files in this folder and run the program again.");
            return;
        }

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Build the XFDF file name (same base name, .xfdf extension)
                string xfdfFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xfdf";
                string xfdfTempPath = Path.Combine(Path.GetTempPath(), xfdfFileName);
                string xfdfArchivePath = Path.Combine(archiveFolder, xfdfFileName);

                // Use PdfAnnotationEditor to work with annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the PDF file
                    editor.BindPdf(pdfPath);

                    // Export all annotations to an XFDF file – the overload expects a Stream
                    using (FileStream xfdfStream = new FileStream(xfdfTempPath, FileMode.Create, FileAccess.Write))
                    {
                        editor.ExportAnnotationsToXfdf(xfdfStream);
                    }

                    // Delete all annotations from the PDF
                    editor.DeleteAnnotations();

                    // Save the modified PDF (overwrite original)
                    editor.Save(pdfPath);
                }

                // Move the exported XFDF file to the archive folder
                if (File.Exists(xfdfArchivePath))
                {
                    // If a file with the same name already exists, overwrite it
                    File.Delete(xfdfArchivePath);
                }
                File.Move(xfdfTempPath, xfdfArchivePath);

                Console.WriteLine($"Processed '{Path.GetFileName(pdfPath)}' – annotations archived to '{xfdfArchivePath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch annotation export, deletion, and archiving completed.");
    }
}
