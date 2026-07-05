using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class PdfUtility
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage:");
            Console.Error.WriteLine("  delete <inputPdf> <outputPdf> <pageNumbersCommaSeparated>");
            Console.Error.WriteLine("  flatten <inputPdf> <outputPdf>");
            Console.Error.WriteLine("  export <inputPdf> <outputXfdf>");
            return;
        }

        string operation = args[0].ToLowerInvariant();
        string inputPath = args[1];
        string outputPath = args[2];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            switch (operation)
            {
                case "delete":
                    // Expect page numbers as a comma‑separated list in args[3]
                    if (args.Length < 4)
                    {
                        Console.Error.WriteLine("Delete operation requires a list of page numbers.");
                        return;
                    }
                    int[] pagesToDelete = args[3]
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => int.Parse(s.Trim()))
                        .ToArray();

                    // PdfFileEditor does NOT implement IDisposable, so do not use a using block
                    PdfFileEditor editor = new PdfFileEditor();
                    editor.Delete(inputPath, pagesToDelete, outputPath);
                    Console.WriteLine($"Pages deleted and saved to '{outputPath}'.");
                    break;

                case "flatten":
                    // Flatten all annotations in the document
                    using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
                    {
                        annotationEditor.BindPdf(inputPath);
                        annotationEditor.FlatteningAnnotations(); // flatten all
                        annotationEditor.Save(outputPath);
                    }
                    Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
                    break;

                case "export":
                    // Export all annotations to XFDF – the overload expects a Stream
                    using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
                    using (FileStream xfdfStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        annotationEditor.BindPdf(inputPath);
                        annotationEditor.ExportAnnotationsToXfdf(xfdfStream);
                    }
                    Console.WriteLine($"Annotations exported to XFDF file '{outputPath}'.");
                    break;

                default:
                    Console.Error.WriteLine($"Unsupported operation: {operation}");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
