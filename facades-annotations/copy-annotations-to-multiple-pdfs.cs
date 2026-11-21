using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdfPath = "template.pdf";          // PDF containing the source annotations
        const string annotationsXfdf = "template_annotations.xfdf"; // Temporary XFDF file
        const string targetsFolder   = "TargetPdfs";          // Folder with PDFs to receive annotations
        const string outputFolder    = "AnnotatedPdfs";       // Folder where annotated PDFs will be saved

        // Verify required files/folders exist
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }
        if (!Directory.Exists(targetsFolder))
        {
            Console.Error.WriteLine($"Target folder not found: {targetsFolder}");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // Step 1: Export annotations from the template PDF to an XFDF file.
        // Use Document.ExportAnnotationsToXfdf – the older ExportAnnotations method
        // does not exist in recent Aspose.Pdf versions.
        // -----------------------------------------------------------------
        try
        {
            using (var templateDoc = new Document(templatePdfPath))
            using (var xfdfStream = new FileStream(annotationsXfdf, FileMode.Create, FileAccess.Write))
            {
                templateDoc.ExportAnnotationsToXfdf(xfdfStream);
            }
            Console.WriteLine($"Annotations exported to '{annotationsXfdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to export annotations: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Iterate over each target PDF, copy it to the output folder,
        // and import the previously exported annotations.
        // -----------------------------------------------------------------
        string[] targetFiles = Directory.GetFiles(targetsFolder, "*.pdf");
        foreach (string targetPath in targetFiles)
        {
            string fileName = Path.GetFileName(targetPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Copy original PDF to output location (preserves original file)
                File.Copy(targetPath, outputPath, overwrite: true);

                // Import annotations from XFDF into the copied PDF.
                using (var targetDoc = new Document(outputPath))
                using (var xfdfRead = new FileStream(annotationsXfdf, FileMode.Open, FileAccess.Read))
                {
                    // ImportAnnotationsFromXfdf is the current API for XFDF import.
                    targetDoc.ImportAnnotationsFromXfdf(xfdfRead);
                    targetDoc.Save(outputPath);
                }

                Console.WriteLine($"Annotations imported into '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // Optional cleanup: delete the temporary XFDF file.
        // -----------------------------------------------------------------
        try
        {
            if (File.Exists(annotationsXfdf))
                File.Delete(annotationsXfdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary XFDF file: {ex.Message}");
        }

        Console.WriteLine("Annotation copying completed.");
    }
}
