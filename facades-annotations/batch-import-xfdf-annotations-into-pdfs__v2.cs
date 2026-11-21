using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the application (works cross‑platform)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Directories containing source PDFs, XFDF files and the output PDFs
        string pdfDirectory = Path.Combine(baseDir, "pdfs");
        string xfdfDirectory = Path.Combine(baseDir, "xfdfs");
        string outputDirectory = Path.Combine(baseDir, "output");

        // Ensure the required folders exist (create output folder, warn if source folders are missing)
        if (!Directory.Exists(pdfDirectory))
        {
            Console.Error.WriteLine($"PDF source folder not found: {pdfDirectory}");
            return;
        }
        if (!Directory.Exists(xfdfDirectory))
        {
            Console.Error.WriteLine($"XFDF source folder not found: {xfdfDirectory}");
            return;
        }
        Directory.CreateDirectory(outputDirectory);

        int processed = 0;
        int skipped = 0;
        int failed = 0;

        // Iterate over all PDF files in the source folder
        foreach (string pdfPath in Directory.GetFiles(pdfDirectory, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(xfdfDirectory, fileNameWithoutExt + ".xfdf");

            if (!File.Exists(xfdfPath))
            {
                Console.WriteLine($"No matching XFDF for '{pdfPath}'. Skipping.");
                skipped++;
                continue;
            }

            string outputPdfPath = Path.Combine(outputDirectory, Path.GetFileName(pdfPath));

            try
            {
                // Load the PDF document inside a using block to ensure proper disposal
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Use the Form facade to import XFDF data (the facade works with streams)
                    using (Form formFacade = new Form(pdfDoc))
                    {
                        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                        {
                            formFacade.ImportXfdf(xfdfStream);
                        }
                        // Save the resulting PDF via the facade
                        formFacade.Save(outputPdfPath);
                    }
                }

                Console.WriteLine($"Imported XFDF '{xfdfPath}' into PDF '{outputPdfPath}'.");
                processed++;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to import XFDF for '{pdfPath}': {ex.Message}");
                failed++;
            }
        }

        Console.WriteLine($"Batch XFDF import completed. Processed: {processed}, Skipped: {skipped}, Failed: {failed}.");
    }
}
