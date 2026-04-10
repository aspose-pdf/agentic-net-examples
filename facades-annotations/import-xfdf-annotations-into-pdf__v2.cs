using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Open the PDF, XFDF, and output streams without creating temporary files
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Initialize the annotation editor and bind the PDF stream
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(pdfStream);

            // Import all annotations from the XFDF stream
            editor.ImportAnnotationsFromXfdf(xfdfStream);

            // Save the modified PDF to the output stream
            editor.Save(outputStream);

            // Release resources held by the editor
            editor.Close();
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}