using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        const int maxAttempts = 2;
        int attempt = 0;
        bool deleted = false;

        while (attempt < maxAttempts && !deleted)
        {
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            try
            {
                editor.BindPdf(inputPath);
                editor.DeleteAnnotation(annotationName);
                editor.Save(outputPath);
                deleted = true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Attempt " + (attempt + 1) + " failed: " + ex.Message);
                // If not the last attempt, the loop will retry after re‑binding.
            }
            finally
            {
                editor.Close();
            }
            attempt++;
        }

        if (deleted)
        {
            Console.WriteLine("Annotation deleted and saved to '" + outputPath + "'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete annotation after retries.");
        }
    }
}
