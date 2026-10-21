using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class PdfUtility
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  PdfUtility delete <input.pdf> <output.pdf> <pages>");
            Console.WriteLine("  PdfUtility flatten <input.pdf> <output.pdf>");
            Console.WriteLine("  PdfUtility export <input.pdf> <outputFolder>");
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
                    // Delete specified pages (comma‑separated list, 1‑based)
                    if (args.Length < 4)
                    {
                        Console.Error.WriteLine("Delete operation requires a page list argument.");
                        return;
                    }
                    int[] pagesToDelete = args[3]
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(p => int.Parse(p.Trim()))
                        .ToArray();

                    // NOTE: PdfFileEditor.Delete signature is (inputPath, pages, outputPath)
                    PdfFileEditor fileEditor = new PdfFileEditor();
                    fileEditor.Delete(inputPath, pagesToDelete, outputPath);
                    Console.WriteLine($"Deleted pages saved to '{outputPath}'.");
                    break;

                case "flatten":
                    // Flatten form fields into static content
                    using (Document doc = new Document(inputPath))
                    {
                        // In current Aspose.Pdf versions, flattening is done via the Form object
                        if (doc.Form != null)
                        {
                            doc.Form.Flatten();
                        }
                        doc.Save(outputPath);
                    }
                    Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
                    break;

                case "export":
                    // Export each page as a PNG image into the specified folder
                    string outputFolder = outputPath; // treat third argument as folder for export
                    Directory.CreateDirectory(outputFolder);

                    using (Document doc = new Document(inputPath))
                    {
                        // Use PngDevice (cross‑platform) instead of PdfConverter/ImageSaveOptions
                        var resolution = new Resolution(150);
                        for (int i = 1; i <= doc.Pages.Count; i++)
                        {
                            string imgPath = Path.Combine(outputFolder, $"page_{i}.png");
                            using (FileStream imgStream = new FileStream(imgPath, FileMode.Create))
                            {
                                PngDevice pngDevice = new PngDevice(resolution);
                                pngDevice.Process(doc.Pages[i], imgStream);
                            }
                        }
                    }
                    Console.WriteLine($"Pages exported as PNG images to folder '{outputFolder}'.");
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
