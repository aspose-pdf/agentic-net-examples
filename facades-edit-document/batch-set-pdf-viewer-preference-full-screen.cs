using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        const string inputDirectory = "input_pdfs";
        // Directory where modified PDFs will be saved
        const string outputDirectory = "output_pdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string sourcePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePath);
            string destinationPath = Path.Combine(outputDirectory, fileName);

            // Use PdfContentEditor to modify viewer preferences
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the PDF file
                editor.BindPdf(sourcePath);
                // Set the viewer preference to full‑screen mode
                editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);
                // Save the modified PDF
                editor.Save(destinationPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }
    }
}