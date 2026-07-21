using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files (relative to the executable folder)
        const string inputDirectory = "InputPdfs";
        // Directory where modified PDFs will be saved
        const string outputDirectory = "OutputPdfs";

        // Resolve full paths based on the current working directory for robustness
        string inputDirFullPath = Path.GetFullPath(inputDirectory);
        string outputDirFullPath = Path.GetFullPath(outputDirectory);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputDirFullPath))
        {
            Console.WriteLine($"Input directory not found: '{inputDirFullPath}'. Please create it and place PDF files inside.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirFullPath);

        // Process each PDF file in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDirFullPath, "*.pdf"))
        {
            // Determine the output file path (keeps the original file name)
            string outputPath = Path.Combine(outputDirFullPath, Path.GetFileName(inputPath));

            // Create a PdfContentEditor instance (no IDisposable, so no using block needed)
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the source PDF file to the editor
            editor.BindPdf(inputPath);

            // Set the viewer preference to full‑screen mode
            editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);

            // Save the modified PDF to the output location
            editor.Save(outputPath);
        }

        Console.WriteLine("All PDFs have been updated with full‑screen viewer preference.");
    }
}
