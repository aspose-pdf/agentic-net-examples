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
            Console.Error.WriteLine("  delete <input.pdf> <output.pdf> <pages(comma separated)>");
            Console.Error.WriteLine("  flatten <input.pdf> <output.pdf>");
            Console.Error.WriteLine("  export <input.pdf> <output.xfdf>");
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
                    // Expect pages list as fourth argument
                    if (args.Length < 4)
                    {
                        Console.Error.WriteLine("Missing pages argument for delete operation.");
                        return;
                    }
                    int[] pagesToDelete = args[3]
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(p => int.Parse(p.Trim()))
                        .ToArray();

                    // PdfFileEditor does NOT implement IDisposable – instantiate directly
                    var editor = new PdfFileEditor();
                    editor.Delete(inputPath, pagesToDelete, outputPath);
                    Console.WriteLine($"Pages deleted. Output saved to '{outputPath}'.");
                    break;

                case "flatten":
                    // Flatten all annotations using PdfAnnotationEditor (implements IDisposable)
                    using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
                    {
                        annotationEditor.BindPdf(inputPath);
                        annotationEditor.FlatteningAnnotations(); // flatten all annotations
                        annotationEditor.Save(outputPath);
                    }
                    Console.WriteLine($"Annotations flattened. Output saved to '{outputPath}'.");
                    break;

                case "export":
                    // Export all annotations to XFDF
                    using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
                    {
                        annotationEditor.BindPdf(inputPath);
                        using (FileStream xfdfStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            annotationEditor.ExportAnnotationsToXfdf(xfdfStream);
                        }
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
