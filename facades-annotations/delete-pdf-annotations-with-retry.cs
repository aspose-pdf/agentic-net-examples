using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class AnnotationDeletionHelper
{
    /// <summary>
    /// Deletes all annotations from a PDF file. If the first attempt fails,
    /// the PDF is rebound and the operation is retried up to <paramref name="maxRetries"/> times.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="outputPdf">Path where the resulting PDF will be saved.</param>
    /// <param name="maxRetries">Maximum number of retry attempts (default = 3).</param>
    public static void DeleteAllAnnotationsWithRetry(string inputPdf, string outputPdf, int maxRetries = 3)
    {
        if (string.IsNullOrWhiteSpace(inputPdf))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdf));

        if (string.IsNullOrWhiteSpace(outputPdf))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdf));

        if (!File.Exists(inputPdf))
            throw new FileNotFoundException($"Input file not found: {inputPdf}");

        int attempt = 0;

        while (true)
        {
            attempt++;

            try
            {
                // Bind, delete annotations and save.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(inputPdf);
                    editor.DeleteAnnotations();
                    editor.Save(outputPdf);
                }

                // Success – exit loop.
                break;
            }
            catch (Exception ex) when (attempt < maxRetries)
            {
                // Log and retry.
                Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                Console.Error.WriteLine("Rebinding PDF and retrying...");
                // Loop will retry; using block ensures disposal.
            }
        }
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console application.
    /// Usage: dotnet run <inputPdfPath> <outputPdfPath> [maxRetries]
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath> [maxRetries]");
            return;
        }

        string inputPdf = args[0];
        string outputPdf = args[1];
        int maxRetries = 3;
        if (args.Length >= 3 && int.TryParse(args[2], out int parsed))
            maxRetries = parsed;

        try
        {
            AnnotationDeletionHelper.DeleteAllAnnotationsWithRetry(inputPdf, outputPdf, maxRetries);
            Console.WriteLine("Annotation deletion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Operation failed: {ex.Message}");
        }
    }
}
