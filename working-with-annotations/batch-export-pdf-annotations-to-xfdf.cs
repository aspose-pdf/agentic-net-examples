using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class ExportAnnotationsBatch
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where XFDF files will be written
        const string outputFolder = @"C:\XfdfOutput";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Derive output XFDF file name from the PDF name
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(outputFolder, fileNameWithoutExt + ".xfdf");

            // Load the PDF and export its annotations using the XFDF save format.
            // The older Aspose.Pdf versions do not expose SaveFormat.Xfdf, so we rely on
            // the overload that infers the format from the file extension.
            using (Document doc = new Document(pdfPath))
            {
                doc.Save(xfdfPath); // Saves as XFDF because the extension is .xfdf
            }

            Console.WriteLine($"Exported annotations: '{pdfPath}' → '{xfdfPath}'");
        }

        Console.WriteLine("Batch export completed.");
    }
}
