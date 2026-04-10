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
            Console.Error.WriteLine("  delete <input.pdf> <output.pdf> <pages(comma-separated)>");
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
                    if (args.Length < 4)
                    {
                        Console.Error.WriteLine("Delete operation requires a pages list argument.");
                        return;
                    }
                    int[] pagesToDelete = ParsePageNumbers(args[3]);
                    DeletePages(inputPath, pagesToDelete, outputPath);
                    Console.WriteLine($"Pages deleted. Output saved to '{outputPath}'.");
                    break;

                case "flatten":
                    FlattenAnnotations(inputPath, outputPath);
                    Console.WriteLine($"Annotations flattened. Output saved to '{outputPath}'.");
                    break;

                case "export":
                    ExportAnnotations(inputPath, outputPath);
                    Console.WriteLine($"Annotations exported to XFDF file '{outputPath}'.");
                    break;

                default:
                    Console.Error.WriteLine($"Unknown operation: {operation}");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Parses a comma‑separated list of page numbers (e.g., "1,3,5")
    private static int[] ParsePageNumbers(string pages)
    {
        return pages
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => int.Parse(p.Trim()))
            .ToArray();
    }

    // Deletes specified pages using PdfFileEditor (no using – it does not implement IDisposable)
    private static void DeletePages(string inputFile, int[] pages, string outputFile)
    {
        var editor = new PdfFileEditor();
        editor.Delete(inputFile, pages, outputFile);
    }

    // Flattens all annotations using PdfAnnotationEditor (no using – it does not implement IDisposable)
    private static void FlattenAnnotations(string inputFile, string outputFile)
    {
        var editor = new PdfAnnotationEditor();
        editor.BindPdf(inputFile);
        editor.FlatteningAnnotations(); // flatten all annotation types
        editor.Save(outputFile);
    }

    // Exports all annotations to an XFDF file using PdfAnnotationEditor
    private static void ExportAnnotations(string inputFile, string xfdfOutput)
    {
        var editor = new PdfAnnotationEditor();
        editor.BindPdf(inputFile);
        using (FileStream fs = new FileStream(xfdfOutput, FileMode.Create, FileAccess.Write))
        {
            editor.ExportAnnotationsToXfdf(fs);
        }
    }
}
