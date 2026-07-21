using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath) || !File.Exists(xfdfPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Open the PDF and XFDF streams without creating temporary files
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document from the stream
            editor.BindPdf(pdfStream);

            // Import all annotations from the XFDF stream
            editor.ImportAnnotationsFromXfdf(xfdfStream);

            // Save the updated PDF to the desired output location
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported successfully to '{outputPath}'.");
    }
}