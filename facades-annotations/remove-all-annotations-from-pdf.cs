using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the annotation editor, bind the PDF, delete all annotations, and save
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);          // Load the PDF into the editor
        editor.DeleteAnnotations();        // Remove every annotation in the document
        editor.Save(outputPdf);            // Persist the cleaned PDF
        editor.Close();                    // Release resources held by the facade

        Console.WriteLine($"All annotations removed. Saved to '{outputPdf}'.");
    }
}