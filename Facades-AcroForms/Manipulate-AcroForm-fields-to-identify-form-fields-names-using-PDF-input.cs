using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for WidgetAnnotation

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-pdf> [output-pdf]");
            return;
        }

        string inputPath = args[0];
        string? outputPath = args.Length > 1 ? args[1] : null; // nullable to reflect optional argument

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Check if the document contains a form
            if (pdfDocument.Form == null || pdfDocument.Form.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any AcroForm fields.");
            }
            else
            {
                Console.WriteLine("AcroForm field names found in the document:");
                // Iterate over all fields in the form
                foreach (WidgetAnnotation widget in pdfDocument.Form)
                {
                    // The field name can be obtained via FullName, PartialName, or Name.
                    // FullName provides the fully qualified hierarchical name.
                    string fieldName = widget.FullName ?? widget.Name ?? "(unnamed)";
                    Console.WriteLine($"- {fieldName}");
                }
            }

            // If an output path is provided, save a copy of the document
            if (!string.IsNullOrWhiteSpace(outputPath))
            {
                // Resolve full path and directory safely
                string fullOutputPath = Path.GetFullPath(outputPath);
                string? outputDir = Path.GetDirectoryName(fullOutputPath);

                // Path.GetDirectoryName can return null (e.g., when only a file name is supplied).
                // Fallback to the current working directory to avoid passing null to CreateDirectory.
                if (string.IsNullOrEmpty(outputDir))
                {
                    outputDir = Directory.GetCurrentDirectory();
                }

                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Save the document
                pdfDocument.Save(fullOutputPath);
                Console.WriteLine($"Document saved to: {fullOutputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}
