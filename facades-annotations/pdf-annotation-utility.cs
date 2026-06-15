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

        try
        {
            switch (operation)
            {
                case "delete":
                    // args: delete input.pdf output.pdf 1,3,5
                    if (args.Length != 4)
                    {
                        Console.Error.WriteLine("Delete operation requires 4 arguments.");
                        return;
                    }
                    DeletePages(args[1], args[2], args[3]);
                    break;

                case "flatten":
                    // args: flatten input.pdf output.pdf
                    FlattenAnnotations(args[1], args[2]);
                    break;

                case "export":
                    // args: export input.pdf output.xfdf
                    ExportAnnotations(args[1], args[2]);
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

    static void DeletePages(string inputPath, string outputPath, string pagesCsv)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        int[] pages = pagesCsv
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => int.Parse(p.Trim()))
            .ToArray();

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it without a using block.
        PdfFileEditor editor = new PdfFileEditor();
        // Use the stream‑based overload to avoid the string‑to‑Stream conversion error.
        using (FileStream inputStream = File.OpenRead(inputPath))
        using (FileStream outputStream = File.Create(outputPath))
        {
            editor.Delete(inputStream, pages, outputStream);
        }

        Console.WriteLine($"Pages deleted. Output saved to '{outputPath}'.");
    }

    static void FlattenAnnotations(string inputPath, string outputPath)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable, so a using block is appropriate.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.FlatteningAnnotations(); // flatten all annotations
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations flattened. Output saved to '{outputPath}'.");
    }

    static void ExportAnnotations(string inputPath, string outputXfdfPath)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            // The ExportAnnotationsToXfdf overload expects a Stream, so we provide a FileStream.
            using (FileStream xfdfStream = File.Create(outputXfdfPath))
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Annotations exported to XFDF file '{outputXfdfPath}'.");
    }
}
