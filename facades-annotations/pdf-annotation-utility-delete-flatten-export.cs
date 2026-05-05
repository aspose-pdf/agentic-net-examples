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

        try
        {
            switch (operation)
            {
                case "delete":
                    // args: inputPdf outputPdf pages (e.g., 1,3,5)
                    if (args.Length < 4)
                    {
                        Console.Error.WriteLine("Delete operation requires page numbers.");
                        return;
                    }
                    string deleteInput = args[1];
                    string deleteOutput = args[2];
                    int[] pagesToDelete = args[3]
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(p => int.Parse(p.Trim()))
                        .ToArray();

                    // PdfFileEditor does not implement IDisposable; direct usage is fine.
                    PdfFileEditor deleter = new PdfFileEditor();
                    deleter.Delete(deleteInput, pagesToDelete, deleteOutput);
                    Console.WriteLine($"Deleted pages and saved to '{deleteOutput}'.");
                    break;

                case "flatten":
                    // args: inputPdf outputPdf
                    string flattenInput = args[1];
                    string flattenOutput = args[2];

                    using (PdfAnnotationEditor flattener = new PdfAnnotationEditor())
                    {
                        flattener.BindPdf(flattenInput);
                        flattener.FlatteningAnnotations();
                        flattener.Save(flattenOutput);
                    }
                    Console.WriteLine($"Annotations flattened and saved to '{flattenOutput}'.");
                    break;

                case "export":
                    // args: inputPdf outputXfdf
                    string exportInput = args[1];
                    string exportOutput = args[2];

                    using (PdfAnnotationEditor exporter = new PdfAnnotationEditor())
                    {
                        exporter.BindPdf(exportInput);
                        using (FileStream xfdfStream = new FileStream(exportOutput, FileMode.Create, FileAccess.Write))
                        {
                            exporter.ExportAnnotationsToXfdf(xfdfStream);
                        }
                    }
                    Console.WriteLine($"Annotations exported to XFDF file '{exportOutput}'.");
                    break;

                default:
                    Console.Error.WriteLine($"Unknown operation '{operation}'. Supported: delete, flatten, export.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}