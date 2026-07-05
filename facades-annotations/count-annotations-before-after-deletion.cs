using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor and bind the PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Count annotations before deletion
        int beforeCount = 0;
        foreach (Page page in editor.Document.Pages)
        {
            beforeCount += page.Annotations.Count;
        }

        // Delete all annotations in the document
        editor.DeleteAnnotations();

        // Count annotations after deletion
        int afterCount = 0;
        foreach (Page page in editor.Document.Pages)
        {
            afterCount += page.Annotations.Count;
        }

        // Output the results
        Console.WriteLine($"Annotations before deletion: {beforeCount}");
        Console.WriteLine($"Annotations after deletion:  {afterCount}");

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources
        editor.Close();
    }
}