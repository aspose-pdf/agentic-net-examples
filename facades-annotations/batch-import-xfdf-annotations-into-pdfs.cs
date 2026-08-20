using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Resolve input / output folders.
        // Use the application base directory as a safe fallback so the sample can
        // run on any machine without requiring the user to create "C:\Input" etc.
        // ---------------------------------------------------------------------
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string pdfInputDir   = Path.Combine(baseDir, "Input", "Pdf");
        string xfdfInputDir  = Path.Combine(baseDir, "Input", "Xfdf");
        string pdfOutputDir  = Path.Combine(baseDir, "Output", "Pdf");

        // Ensure the directories exist – if they do not, create them so the
        // program does not throw a DirectoryNotFoundException.
        Directory.CreateDirectory(pdfInputDir);
        Directory.CreateDirectory(xfdfInputDir);
        Directory.CreateDirectory(pdfOutputDir);

        // ---------------------------------------------------------------------
        // Validate that there is at least one PDF to process.  This gives a clear
        // message instead of silently doing nothing.
        // ---------------------------------------------------------------------
        string[] pdfFiles = Directory.GetFiles(pdfInputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{pdfInputDir}'. Place PDFs there and rerun the program.");
            return;
        }

        // ---------------------------------------------------------------------
        // Process each PDF – import the matching XFDF file (same name, .xfdf).
        // All I/O is wrapped in try/catch so a single bad file does not abort the
        // whole batch.
        // ---------------------------------------------------------------------
        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(xfdfInputDir, baseName + ".xfdf");

            if (!File.Exists(xfdfPath))
            {
                Console.WriteLine($"[Skip] No XFDF found for '{baseName}'. Expected at '{xfdfPath}'.");
                continue;
            }

            try
            {
                using (Document pdfDoc = new Document(pdfPath))
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfDoc);
                    editor.ImportAnnotationsFromXfdf(xfdfPath);

                    string outputPath = Path.Combine(pdfOutputDir, Path.GetFileName(pdfPath));
                    editor.Save(outputPath);

                    Console.WriteLine($"[Success] Imported XFDF into '{outputPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to process '{pdfPath}'. Exception: {ex.Message}");
            }
        }

        Console.WriteLine("Batch import completed.");
    }
}
