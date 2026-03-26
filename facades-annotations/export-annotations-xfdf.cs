using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document document = new Document(inputPath))
        {
            // Initialize the annotation editor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(document);

                // Export annotations from pages 1 to 2 into an in‑memory XFDF stream
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    // Pass null for annotation types to export all types (or specify a string[] of types)
                    editor.ExportAnnotationsXfdf(xfdfStream, 1, 2, (string[])null);
                    xfdfStream.Position = 0; // Reset for reading or further processing

                    // Example: read the XFDF content as a string (optional)
                    using (StreamReader reader = new StreamReader(xfdfStream))
                    {
                        string xfdfContent = reader.ReadToEnd();
                        Console.WriteLine($"Exported XFDF size: {xfdfContent.Length} characters");
                    }
                }
            }
        }
    }
}