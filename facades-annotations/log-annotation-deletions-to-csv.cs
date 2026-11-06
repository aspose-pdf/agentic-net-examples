using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string logFilePath = "annotation_deletions.log";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document to obtain page count and existing annotations
            using (Document doc = new Document(inputPdfPath))
            {
                int pageCount = doc.Pages.Count;

                // Bind the PDF to the annotation editor (PdfAnnotationEditor does NOT implement IDisposable)
                PdfAnnotationEditor annotEditor = new PdfAnnotationEditor();
                annotEditor.BindPdf(inputPdfPath);

                // Open the log file (CSV format) and write a header line
                using (StreamWriter logWriter = new StreamWriter(logFilePath, false, Encoding.UTF8))
                {
                    logWriter.WriteLine("Name,Type,PageNumber");

                    // Iterate through each page and delete its annotations while logging the details
                    for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                    {
                        // Retrieve all annotations on the current page using the Document API
                        var pageAnnotations = doc.Pages[pageNum].Annotations;

                        foreach (Annotation annotation in pageAnnotations)
                        {
                            // Log the deletion action (sanitize commas for CSV safety)
                            string safeName = annotation.Name?.Replace(",", " ") ?? "<no-name>";
                            string safeType = annotation.GetType().Name?.Replace(",", " ");
                            logWriter.WriteLine($"{safeName},{safeType},{pageNum}");

                            // Delete the annotation by its name using the editor (current API overload takes only the name)
                            if (!string.IsNullOrEmpty(annotation.Name))
                            {
                                annotEditor.DeleteAnnotation(annotation.Name);
                            }
                        }
                    }
                }

                // Save the modified PDF to the output path
                annotEditor.Save(outputPdfPath);
                Console.WriteLine($"Annotations deleted and logged to '{logFilePath}'. Output PDF saved as '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
