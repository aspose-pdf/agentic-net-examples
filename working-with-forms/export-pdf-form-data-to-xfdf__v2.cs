using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least the PDF file path. Optional second argument is the output XFDF path.
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: PdfFormExport <pdfPath> [outputXfdfPath]");
            return;
        }

        string pdfPath = args[0];
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Determine output file name – same name with .xfdf extension if not supplied.
        string outputPath = args.Length > 1 ? args[1] : Path.ChangeExtension(pdfPath, ".xfdf");

        // Load the PDF document and export its form (annotation) data directly to a file stream.
        using (Document pdfDocument = new Document(pdfPath))
        {
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // ExportAnnotationsToXfdf writes XFDF (XML) to the provided stream.
                pdfDocument.ExportAnnotationsToXfdf(fs);
            }
        }

        Console.WriteLine($"Form data exported to: {outputPath}");
    }
}
