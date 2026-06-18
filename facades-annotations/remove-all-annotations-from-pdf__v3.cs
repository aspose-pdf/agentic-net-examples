using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output PDF path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfCleaner <inputPdfPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Process the PDF: remove all annotations and write the cleaned PDF
        using (FileStream inputStream = File.OpenRead(inputPath))
        using (MemoryStream outputStream = new MemoryStream())
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF stream to the editor
            editor.BindPdf(inputStream);

            // Delete every annotation in the document
            editor.DeleteAnnotations();

            // Save the cleaned document into the output memory stream
            editor.Save(outputStream);

            // Reset the position so we can read from the beginning
            outputStream.Position = 0;

            // Write the cleaned PDF to the target file
            using (FileStream fileOut = File.Create(outputPath))
            {
                outputStream.CopyTo(fileOut);
            }
        }

        Console.WriteLine($"Cleaned PDF saved to: {outputPath}");
    }
}
