using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38"; // example annotation ID

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // First attempt to delete the annotation
        bool success = TryDeleteAnnotation(inputPath, outputPath, annotationName);

        // If the first attempt fails, rebind the PDF and retry once
        if (!success)
        {
            Console.WriteLine("Retrying annotation deletion after rebinding PDF.");
            success = TryDeleteAnnotation(inputPath, outputPath, annotationName);
            if (!success)
            {
                Console.Error.WriteLine("Failed to delete annotation after retry.");
            }
        }
    }

    static bool TryDeleteAnnotation(string inputFile, string outputFile, string annotName)
    {
        try
        {
            // Bind the PDF, delete the annotation, and save the result
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputFile);               // Load PDF
                editor.DeleteAnnotation(annotName);      // Delete specific annotation
                editor.Save(outputFile);                 // Save modified PDF
            }

            Console.WriteLine($"Annotation deleted and saved to '{outputFile}'.");
            return true;
        }
        catch (Exception ex)
        {
            // Log the error and indicate failure
            Console.Error.WriteLine($"Error during annotation deletion: {ex.Message}");
            return false;
        }
    }
}