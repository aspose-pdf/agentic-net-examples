using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the XFDF file containing reviewer comments
        const string xfdfPath = "comments.xfdf";

        // List of PDF files to which the comments will be applied
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Directory where annotated PDFs will be saved
        const string outputDir = "Output";

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF file not found: {pdfPath}");
                continue;
            }

            string outputPath = Path.Combine(outputDir,
                Path.GetFileNameWithoutExtension(pdfPath) + "_annotated.pdf");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Initialize the annotation editor facade
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        // Bind the PDF document to the editor
                        editor.BindPdf(doc);

                        // Import all annotations from the XFDF file
                        editor.ImportAnnotationsFromXfdf(xfdfPath);

                        // Save the annotated PDF
                        editor.Save(outputPath);
                    }
                }

                Console.WriteLine($"Annotated PDF saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}