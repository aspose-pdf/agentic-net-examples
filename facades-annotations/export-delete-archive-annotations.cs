using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing PDF files
        string inputFolder = "input";
        // Output folder for cleaned PDFs
        string outputFolder = "output";
        // Archive file for all XFDF files
        string archivePath = "annotations.zip";

        // Ensure input and output folders exist
        if (!Directory.Exists(inputFolder))
        {
            // Create the folder so the program can run without throwing an exception.
            // In a real‑world scenario you might want to abort or prompt the user.
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder '{inputFolder}' did not exist and was created. Place PDF files there and re‑run the program.");
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each PDF file in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfFile in pdfFiles)
        {
            // Determine file names for XFDF and cleaned PDF
            string pdfFileName = Path.GetFileName(pdfFile);
            string xfdfFileName = Path.ChangeExtension(pdfFileName, ".xfdf");
            string xfdfFilePath = Path.Combine(outputFolder, xfdfFileName); // store XFDF next to cleaned PDF
            string cleanedPdfFileName = Path.GetFileNameWithoutExtension(pdfFileName) + "_clean.pdf";
            string cleanedPdfPath = Path.Combine(outputFolder, cleanedPdfFileName);

            // Export annotations to XFDF and delete them from the document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfFile);

                // Export all annotations to XFDF file
                using (FileStream xfdfStream = File.Create(xfdfFilePath))
                {
                    editor.ExportAnnotationsToXfdf(xfdfStream);
                }

                // Delete all annotations
                editor.DeleteAnnotations();

                // Save the PDF without annotations
                editor.Save(cleanedPdfPath);
            }
        }

        // Archive all generated XFDF files into a zip archive
        using (FileStream zipStream = new FileStream(archivePath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            // All XFDF files are stored in the output folder
            string[] xfdfFiles = Directory.GetFiles(outputFolder, "*.xfdf");
            foreach (string xfdfFile in xfdfFiles)
            {
                string entryName = Path.GetFileName(xfdfFile);
                ZipArchiveEntry entry = archive.CreateEntry(entryName);
                using (Stream entryStream = entry.Open())
                using (FileStream sourceStream = File.OpenRead(xfdfFile))
                {
                    sourceStream.CopyTo(entryStream);
                }
            }
        }

        // Optional: delete individual XFDF files after archiving
        // string[] xfdfFilesToDelete = Directory.GetFiles(outputFolder, "*.xfdf");
        // foreach (string file in xfdfFilesToDelete) { File.Delete(file); }

        Console.WriteLine("Annotation export, deletion, and archiving completed.");
    }
}
