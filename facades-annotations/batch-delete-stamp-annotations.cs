using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs
        const string inputDir = "pdfs";
        // Output directory for processed PDFs
        const string outputDir = "output";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outPath = Path.Combine(outputDir, fileName);

            try
            {
                // Initialize the annotation editor and bind the PDF
                PdfAnnotationEditor editor = new PdfAnnotationEditor();
                editor.BindPdf(pdfPath);

                // Delete all stamp annotations in the document
                editor.DeleteAnnotations("Stamp");

                // Save the modified PDF to the output location
                editor.Save(outPath);

                // Release resources held by the editor
                editor.Close();

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}