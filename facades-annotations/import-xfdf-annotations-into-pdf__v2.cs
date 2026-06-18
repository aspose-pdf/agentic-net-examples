using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and XFDF data.
        const string pdfInputPath  = "source.pdf";
        const string xfdfInputPath = "annotations.xfdf";
        const string pdfOutputPath = "output.pdf";

        // Validate input files exist.
        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(xfdfInputPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfInputPath}");
            return;
        }

        // Open the PDF file as a read‑only stream.
        using (FileStream pdfStream = File.OpenRead(pdfInputPath))
        // Create the annotation editor facade.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF stream to the editor.
            editor.BindPdf(pdfStream);

            // Open the XFDF data as a stream and import all annotations.
            using (FileStream xfdfStream = File.OpenRead(xfdfInputPath))
            {
                editor.ImportAnnotationsFromXfdf(xfdfStream);
            }

            // Save the modified PDF to the output file via a write stream.
            using (FileStream outStream = new FileStream(pdfOutputPath, FileMode.Create, FileAccess.Write))
            {
                editor.Save(outStream);
            }
        }

        Console.WriteLine($"Annotations imported and saved to '{pdfOutputPath}'.");
    }
}