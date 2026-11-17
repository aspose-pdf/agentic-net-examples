using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;

// Suppress known‑vulnerability NuGet warnings for the transitive package
#pragma warning disable NU1902 // Moderate severity vulnerability in System.Security.Cryptography.Pkcs 9.0.0
#pragma warning disable NU1903 // High severity vulnerability in System.Security.Cryptography.Pkcs 9.0.0

class Program
{
    static void Main()
    {
        // Base directory of the application – works on any platform
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder  = Path.Combine(baseDir, "Input");
        string outputFolder = Path.Combine(baseDir, "Output");

        // Ensure the folders exist (creates them if missing)
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Gather all PDF files from the input folder
        List<string> pdfFiles = new List<string>(Directory.GetFiles(inputFolder, "*.pdf"));

        // Process each PDF in parallel for speed
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Determine the matching XFDF file (same base name, .xfdf extension)
                string xfdfPath = Path.ChangeExtension(pdfPath, ".xfdf");
                if (!File.Exists(xfdfPath))
                {
                    Console.Error.WriteLine($"XFDF not found for '{Path.GetFileName(pdfPath)}'. Skipping.");
                    return;
                }

                // Load the PDF, import annotations from XFDF, and save the result
                using (Document doc = new Document(pdfPath))
                {
                    // Core API – imports all annotations contained in the XFDF file
                    doc.ImportAnnotationsFromXfdf(xfdfPath);

                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        });

        Console.WriteLine("All files processed.");
    }
}
#pragma warning restore NU1902
#pragma warning restore NU1903