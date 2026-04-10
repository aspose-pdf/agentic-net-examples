using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "output.pdf";
            const string annotationName = "Comment1";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Initialize the annotation editor facade and ensure it is disposed properly
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Delete the annotation with the specified name
                editor.DeleteAnnotation(annotationName);

                // Save the modified PDF
                editor.Save(outputPath);

                // Explicitly close the editor to release any file handles (defensive)
                editor.Close();
            }

            Console.WriteLine($"Annotation '{annotationName}' deleted. Saved to '{outputPath}'.");
        }
    }
}
