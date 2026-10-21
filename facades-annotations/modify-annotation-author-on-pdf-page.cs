using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the annotation
        const string inputPath  = "input.pdf";
        // Output PDF with the modified annotation author
        const string outputPath = "output_modified.pdf";

        // Page number (1‑based) and annotation index (0‑based) to target
        const int pageNumber      = 3; // third page
        const int annotationIndex = 2; // second annotation on that page

        // Original author of the annotation (empty string matches any author)
        const string sourceAuthor = "";
        // New author name to set
        const string newAuthor    = "John Doe";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor from Aspose.Pdf.Facades to modify the annotation author
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPath);

            // Modify the author of the specified annotation
            // Signature: ModifyAnnotationsAuthor(int pageNumber, int annotationIndex, string srcAuthor, string desAuthor)
            editor.ModifyAnnotationsAuthor(pageNumber, annotationIndex, sourceAuthor, newAuthor);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation author updated and saved to '{outputPath}'.");
    }
}