using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, input FDF path, output PDF path
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: <program> <inputPdf> <inputFdf> <outputPdf>");
            return;
        }

        string inputPdfPath = args[0];
        string inputFdfPath = args[1];
        string outputPdfPath = args[2];

        // Validate file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Error: PDF file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(inputFdfPath))
        {
            Console.WriteLine($"Error: FDF file not found: {inputFdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Open the FDF stream and import its data into the PDF form
            using (Stream fdfStream = File.OpenRead(inputFdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, pdfDocument);
            }

            // Save the modified PDF document
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"PDF saved successfully to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}