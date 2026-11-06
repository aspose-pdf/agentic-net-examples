using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directories for input PDFs, processed PDFs, and temporary XFDF files
        const string inputDir = "InputPdfs";
        const string outputDir = "ProcessedPdfs";
        const string xfdfDir = "XFDF";
        const string archivePath = "AnnotationsArchive.zip";

        // Ensure directories exist
        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);
        Directory.CreateDirectory(xfdfDir);

        // Process each PDF file in the input directory
        foreach (string pdfFile in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFile);
            string processedPath = Path.Combine(outputDir, fileName);
            string xfdfPath = Path.Combine(xfdfDir, Path.GetFileNameWithoutExtension(pdfFile) + ".xfdf");

            // Determine the number of pages in the source PDF (required for ExportAnnotationsXfdf)
            int pageCount;
            using (var tempDoc = new Document(pdfFile))
            {
                pageCount = tempDoc.Pages.Count;
            }

            // Use PdfAnnotationEditor to export, delete, and save the PDF
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF
                editor.BindPdf(pdfFile);

                // Export all annotations to an XFDF file (using the correct overload)
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    // (string[])null selects the overload that imports all annotation types
                    editor.ExportAnnotationsXfdf(xfdfStream, 1, pageCount, (string[])null);
                }

                // Delete all annotations from the PDF
                editor.DeleteAnnotations();

                // Save the cleaned PDF to the output directory
                editor.Save(processedPath);
            }

            Console.WriteLine($"Processed '{fileName}': annotations exported to XFDF and removed.");
        }

        // Archive all generated XFDF files into a single ZIP file
        if (File.Exists(archivePath))
        {
            File.Delete(archivePath);
        }

        ZipFile.CreateFromDirectory(xfdfDir, archivePath);
        Console.WriteLine($"All XFDF files archived to '{archivePath}'.");

        // Optional: clean up temporary XFDF directory after archiving
        // Directory.Delete(xfdfDir, true);
    }
}
