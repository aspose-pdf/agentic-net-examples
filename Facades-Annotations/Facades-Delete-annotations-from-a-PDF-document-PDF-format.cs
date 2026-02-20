using System;
using System.IO;
using Aspose.Pdf.Facades;

class DeletePdfAnnotations
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Create the PdfAnnotationEditor facade
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Load the PDF document
            editor.BindPdf(inputPath);

            // Delete all annotations in the document
            editor.DeleteAnnotations();

            // Save the modified PDF
            editor.Save(outputPath); // document-save rule applied

            // Release resources
            editor.Close();

            Console.WriteLine($"All annotations have been deleted. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}