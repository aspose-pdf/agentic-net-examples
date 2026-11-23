using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Add a SquareAnnotation (used as a generic figure annotation) to the first page
                Page page = doc.Pages[1];
                // Fully qualified Rectangle to avoid ambiguity with System.Drawing
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                SquareAnnotation figure = new SquareAnnotation(page, rect)
                {
                    // Use Contents to store a description; AlternativeText does not exist on SquareAnnotation
                    Contents = "Sample figure annotation"
                };
                page.Annotations.Add(figure);

                // Export all annotations (including the figure) to XFDF using a MemoryStream
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    // Direct export to XFDF – no XfdfExportOptions class required
                    doc.ExportAnnotationsToXfdf(xfdfStream);

                    // Retrieve the XFDF content as a UTF‑8 string
                    string xfdfContent = Encoding.UTF8.GetString(xfdfStream.ToArray());
                    Console.WriteLine("XFDF export:");
                    Console.WriteLine(xfdfContent);
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
