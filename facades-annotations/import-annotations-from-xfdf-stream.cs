using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfInputPath   = "input.pdf";
        const string xfdfInputPath  = "annotations.xfdf";
        const string pdfOutputPath  = "output.pdf";

        // Verify that the source files exist
        if (!File.Exists(pdfInputPath) || !File.Exists(xfdfInputPath))
        {
            Console.Error.WriteLine("One or more input files were not found.");
            return;
        }

        // Open the PDF and XFDF streams, process, and save without creating temp files
        using (FileStream pdfStream  = File.OpenRead(pdfInputPath))
        using (FileStream xfdfStream = File.OpenRead(xfdfInputPath))
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document from the stream
            editor.BindPdf(pdfStream);

            // Import all annotations from the XFDF stream
            editor.ImportAnnotationsFromXfdf(xfdfStream);

            // Save the modified PDF to the desired output file
            editor.Save(pdfOutputPath);
        }

        Console.WriteLine($"Annotations imported successfully. Output saved to '{pdfOutputPath}'.");
    }
}