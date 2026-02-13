using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF, XFDF file, output PDF
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPdfPath> <xfdfPath> <outputPdfPath>");
            return;
        }

        string pdfPath = args[0];
        string xfdfPath = args[1];
        string outputPath = args[2];

        // Verify that the input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDoc = new Document(pdfPath);

            // Import annotations from the XFDF stream into the document
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                XfdfReader.ReadAnnotations(xfdfStream, pdfDoc);
            }

            // Save the updated PDF with the imported annotations
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF saved with imported annotations to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}