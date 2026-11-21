using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        //   operation  inputPath  outputPath  [additional]
        // operation: delete | flatten | export
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <operation> <inputPdf> <outputPath> [options]");
            Console.Error.WriteLine("Operations:");
            Console.Error.WriteLine("  delete <pageNumber>   - delete the specified page");
            Console.Error.WriteLine("  flatten                - flatten form fields");
            Console.Error.WriteLine("  export                 - export PDF to HTML");
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
                    // Requires a page number as the fourth argument
                    if (args.Length < 4)
                    {
                        Console.Error.WriteLine("Delete operation requires a page number.");
                        return;
                    }

                    if (!int.TryParse(args[3], out int pageNumber))
                    {
                        Console.Error.WriteLine("Invalid page number.");
                        return;
                    }

                    // Load PDF, delete the page, and save
                    using (Document doc = new Document(inputPath))
                    {
                        // Aspose.Pdf uses 1‑based page indexing
                        if (pageNumber >= 1 && pageNumber <= doc.Pages.Count)
                        {
                            doc.Pages.Delete(pageNumber);
                            doc.Save(outputPath);
                            Console.WriteLine($"Page {pageNumber} deleted. Saved to '{outputPath}'.");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Page number out of range. Document has {doc.Pages.Count} pages.");
                        }
                    }
                    break;

                case "flatten":
                    // Flatten form fields (make them part of the page content)
                    using (Document doc = new Document(inputPath))
                    {
                        // In recent Aspose.Pdf versions the FormFlattening property was removed.
                        // Use the Form.Flatten() method instead.
                        doc.Form?.Flatten();
                        doc.Save(outputPath);
                        Console.WriteLine($"Form fields flattened. Saved to '{outputPath}'.");
                    }
                    break;

                case "export":
                    // Export PDF to HTML (requires explicit HtmlSaveOptions on all platforms)
                    using (Document doc = new Document(inputPath))
                    {
                        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                        {
                            PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                            RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                        };

                        // HTML conversion uses GDI+ on Windows; on non‑Windows platforms it will throw,
                        // so we catch the specific exception type.
                        try
                        {
                            doc.Save(outputPath, htmlOptions);
                            Console.WriteLine($"PDF exported to HTML at '{outputPath}'.");
                        }
                        catch (TypeInitializationException)
                        {
                            Console.Error.WriteLine("HTML conversion requires GDI+ and is only supported on Windows.");
                        }
                    }
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
}
