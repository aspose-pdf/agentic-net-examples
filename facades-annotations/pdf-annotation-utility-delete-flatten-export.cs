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
            Console.Error.WriteLine("  delete <inputPdf> <outputPdf> <pagesCommaSeparated>");
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
                    if (args.Length < 4)
                    {
                        Console.Error.WriteLine("Delete operation requires a pages list.");
                        return;
                    }
                    // Parse pages (1‑based) from comma‑separated string
                    int[] pagesToDelete = args[3]
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(p => int.Parse(p.Trim()))
                        .ToArray();

                    // Use PdfFileEditor to delete specified pages – use stream overloads
                    PdfFileEditor editor = new PdfFileEditor();
                    using (FileStream inputStream = File.OpenRead(inputPath))
                    using (FileStream outputStream = File.Create(outputPath))
                    {
                        editor.Delete(inputStream, pagesToDelete, outputStream);
                    }
                    Console.WriteLine($"Pages deleted. Output saved to '{outputPath}'.");
                    break;

                case "flatten":
                    // Use PdfAnnotationEditor to flatten all annotations – use stream overloads for saving
                    using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
                    {
                        annotEditor.BindPdf(inputPath);
                        annotEditor.FlatteningAnnotations(); // flatten all annotations
                        using (FileStream outStream = File.Create(outputPath))
                        {
                            annotEditor.Save(outStream);
                        }
                    }
                    Console.WriteLine($"Annotations flattened. Output saved to '{outputPath}'.");
                    break;

                case "export":
                    // Export all annotations to XFDF – use stream overload
                    using (PdfAnnotationEditor exportEditor = new PdfAnnotationEditor())
                    {
                        exportEditor.BindPdf(inputPath);
                        using (FileStream outStream = File.Create(outputPath))
                        {
                            exportEditor.ExportAnnotationsToXfdf(outStream);
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
