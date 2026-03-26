using System;
using System.IO;
using Aspose.Pdf;

namespace PdfUtilityCli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 3)
            {
                Console.Error.WriteLine("Usage: <operation> <input.pdf> <outputOrExportPath> [pageNumber]");
                Console.Error.WriteLine("Operations: delete, flatten, export");
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
                using (Document doc = new Document(inputPath))
                {
                    switch (operation)
                    {
                        case "delete":
                            // Delete a page. If a page number is supplied use it, otherwise delete the last page.
                            int pageNumber = 0;
                            if (args.Length > 3)
                            {
                                int.TryParse(args[3], out pageNumber);
                            }
                            if (pageNumber <= 0 || pageNumber > doc.Pages.Count)
                            {
                                pageNumber = doc.Pages.Count; // delete last page by default
                            }
                            doc.Pages.Delete(pageNumber);
                            doc.Save(outputPath);
                            Console.WriteLine($"Page {pageNumber} deleted. Saved to '{outputPath}'.");
                            break;

                        case "flatten":
                            // Flatten form fields and annotations.
                            doc.Flatten();
                            doc.Save(outputPath);
                            Console.WriteLine($"Document flattened. Saved to '{outputPath}'.");
                            break;

                        case "export":
                            // Export all annotations to an XFDF file.
                            doc.ExportAnnotationsToXfdf(outputPath);
                            Console.WriteLine($"Annotations exported to XFDF file '{outputPath}'.");
                            break;

                        default:
                            Console.Error.WriteLine($"Unsupported operation: {operation}");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
