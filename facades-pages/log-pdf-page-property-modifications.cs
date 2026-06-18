using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "modified.pdf";
        const string logPath = "modifications.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create or overwrite the log file
            using (StreamWriter logWriter = new StreamWriter(logPath, false))
            {
                // Initialize the PdfPageEditor facade
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the source PDF file
                    editor.BindPdf(inputPath);
                    logWriter.WriteLine($"Bound PDF: {inputPath}");

                    // Set Zoom factor
                    editor.Zoom = 0.75f;
                    logWriter.WriteLine($"Set Zoom = {editor.Zoom}");

                    // Set page rotation (must be 0, 90, 180, or 270)
                    editor.Rotation = 90;
                    logWriter.WriteLine($"Set Rotation = {editor.Rotation}");

                    // Set output page size (e.g., A4)
                    editor.PageSize = Aspose.Pdf.PageSize.A4;
                    logWriter.WriteLine($"Set PageSize = {editor.PageSize}");

                    // Set display duration for pages (in seconds)
                    editor.DisplayDuration = 5;
                    logWriter.WriteLine($"Set DisplayDuration = {editor.DisplayDuration}");

                    // Set transition type (integer value – 12 corresponds to Fade)
                    editor.TransitionType = 12; // Fade transition
                    logWriter.WriteLine($"Set TransitionType = {editor.TransitionType} (Fade)");

                    // Apply all changes to the document
                    editor.ApplyChanges();
                    logWriter.WriteLine("Applied changes to document.");

                    // Save the modified PDF
                    editor.Save(outputPath);
                    logWriter.WriteLine($"Saved modified PDF to: {outputPath}");
                }
            }

            Console.WriteLine($"Modifications logged to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
