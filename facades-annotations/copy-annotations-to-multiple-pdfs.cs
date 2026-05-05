using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains the source annotations (relative to the executable folder)
        const string templateFileName = "template.pdf";
        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templateFileName);

        // Verify that the template PDF exists
        if (!File.Exists(templatePath))
        {
            Console.WriteLine($"Error: Template PDF not found at '{templatePath}'.");
            return;
        }

        // Paths of the PDFs that should receive the copied annotations (relative to the executable folder)
        string[] targetFileNames = { "target1.pdf", "target2.pdf", "target3.pdf" };
        // Use a nullable array to make the intent explicit and silence CS8604 warnings
        string?[] targetPaths = new string?[targetFileNames.Length];
        for (int i = 0; i < targetFileNames.Length; i++)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, targetFileNames[i]);
            if (!File.Exists(path))
            {
                Console.WriteLine($"Warning: Target PDF not found at '{path}'. It will be skipped.");
                targetPaths[i] = null; // mark as missing
            }
            else
            {
                targetPaths[i] = path;
            }
        }

        // Directory where the annotated PDFs will be saved
        const string outputDirName = "Output";
        string outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputDirName);
        Directory.CreateDirectory(outputDir);

        // Export all annotations from the template PDF into an in‑memory XFDF stream
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            using (PdfAnnotationEditor templateEditor = new PdfAnnotationEditor())
            {
                templateEditor.BindPdf(templatePath);
                templateEditor.ExportAnnotationsToXfdf(xfdfStream);
            }

            // Prepare the stream for reading
            xfdfStream.Position = 0;

            // Iterate over each target PDF, import the annotations, and save the result
            foreach (string? targetPath in targetPaths)
            {
                if (string.IsNullOrEmpty(targetPath) || !File.Exists(targetPath))
                    continue; // skip missing files

                string fileName = Path.GetFileNameWithoutExtension(targetPath);
                string outputPath = Path.Combine(outputDir, fileName + "_annotated.pdf");

                using (PdfAnnotationEditor targetEditor = new PdfAnnotationEditor())
                {
                    targetEditor.BindPdf(targetPath);
                    // Import the previously exported annotations
                    targetEditor.ImportAnnotationsFromXfdf(xfdfStream);
                    // Save the modified document
                    targetEditor.Save(outputPath);
                }

                // Reset stream position for the next target PDF
                xfdfStream.Position = 0;
            }
        }

        Console.WriteLine("Annotations have been copied to all existing target PDFs.");
    }
}
