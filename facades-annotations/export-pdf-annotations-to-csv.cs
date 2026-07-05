using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationLister
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "annotations.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize the facade (required by the task)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Create CSV file and write header
                using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
                {
                    writer.WriteLine("AnnotationName,PageNumber");

                    // Iterate through all pages (1‑based indexing)
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];
                        // Iterate through annotations on the current page
                        foreach (Annotation annotation in page.Annotations)
                        {
                            string name = annotation.Name ?? string.Empty;
                            writer.WriteLine($"{name},{pageNum}");
                        }
                    }
                }

                Console.WriteLine($"Annotation list saved to '{outputCsvPath}'.");
            }
        }
    }
}