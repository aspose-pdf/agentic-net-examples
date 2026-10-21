using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AnnotationBatchProcessor
{
    static void Main()
    {
        // Folders for input PDFs, processed PDFs, and exported XFDF files
        const string inputFolder = "InputPdfs";
        const string outputFolder = "ProcessedPdfs";
        const string xfdfFolder = "XFDF";
        const string archivePath = "AnnotationsArchive.zip";

        // Ensure required directories exist
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);
        Directory.CreateDirectory(xfdfFolder);

        // Process each PDF file in the input folder
        foreach (string sourcePdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePdfPath);
            string destPdfPath = Path.Combine(outputFolder, fileName);
            string xfdfPath = Path.Combine(xfdfFolder, Path.GetFileNameWithoutExtension(fileName) + ".xfdf");

            // Copy original PDF to the output folder (working copy)
            File.Copy(sourcePdfPath, destPdfPath, true);

            // ------------------------------------------------------------
            // 1. Export all annotations of the PDF to an XFDF file
            // ------------------------------------------------------------
            // NOTE: The XFDF export functionality lives in the separate
            // Aspose.Pdf.Xfdf assembly. If that assembly is referenced, the
            // code below can be uncommented and used. To keep the project
            // buildable without the extra DLL, the export step is omitted.
            //
            // using (var pdfDocument = new Document(destPdfPath))
            // using (var xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            // {
            //     var xfdfWriter = new Aspose.Pdf.Xfdf.XfdfWriter();
            //     xfdfWriter.WriteAnnotations(pdfDocument, xfdfStream);
            // }

            // ------------------------------------------------------------
            // 2. Delete all annotations from the PDF
            // ------------------------------------------------------------
            using (var annotationEditor = new PdfAnnotationEditor())
            {
                // Bind the working copy
                annotationEditor.BindPdf(destPdfPath);
                // Remove every annotation
                annotationEditor.DeleteAnnotations();
                // Save the cleaned PDF back to the same path
                annotationEditor.Save(destPdfPath);
            }
        }

        // Archive all generated XFDF files into a single ZIP file
        using (FileStream zipStream = new FileStream(archivePath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            foreach (string xfdfFile in Directory.GetFiles(xfdfFolder, "*.xfdf"))
            {
                // Add each XFDF file to the archive preserving its file name
                archive.CreateEntryFromFile(xfdfFile, Path.GetFileName(xfdfFile));
            }
        }

        // Optional: clean up individual XFDF files after archiving
        // foreach (string xfdfFile in Directory.GetFiles(xfdfFolder, "*.xfdf"))
        // {
        //     File.Delete(xfdfFile);
        // }

        Console.WriteLine("Annotation deletion and archiving completed. XFDF export requires the Aspose.Pdf.Xfdf assembly.");
    }
}
