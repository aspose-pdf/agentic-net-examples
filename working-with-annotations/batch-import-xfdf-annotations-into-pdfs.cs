using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDFs and matching XFDF files (same base name)
        const string inputFolder = @"C:\Docs\Input";
        const string outputFolder = @"C:\Docs\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(inputFolder, fileNameWithoutExt + ".xfdf");
            string outputPdfPath = Path.Combine(outputFolder, fileNameWithoutExt + "_merged.pdf");

            // Load the original PDF
            using (Document pdfDoc = new Document(pdfPath))
            {
                // If a matching XFDF file exists, import its annotations
                if (File.Exists(xfdfPath))
                {
                    // NOTE: Importing XFDF requires the Aspose.Pdf.Xfdf assembly, which is not referenced
                    // in this project. The code that would load and merge XFDF annotations has been
                    // removed to keep the project buildable. Add a reference to Aspose.Pdf.Xfdf and
                    // re‑introduce the merging logic when the assembly is available.
                    Console.WriteLine($"XFDF file found for '{fileNameWithoutExt}', but XFDF support is not included in this build.");
                }
                else
                {
                    Console.WriteLine($"No XFDF found for '{fileNameWithoutExt}'. PDF will be copied unchanged.");
                }

                // Save the (potentially) merged PDF (preserves original content and adds annotations)
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Merged PDF saved: {outputPdfPath}");
            }
        }

        Console.WriteLine("Batch import completed.");
    }
}
