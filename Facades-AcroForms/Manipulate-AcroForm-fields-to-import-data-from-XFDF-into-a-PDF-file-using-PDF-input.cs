using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // XfdfReader resides here

class Program
{
    static void Main(string[] args)
    {
        // Input PDF and XFDF file paths (adjust as needed)
        const string pdfPath = "input.pdf";
        const string xfdfPath = "data.xfdf";
        const string outputPath = "output.pdf";

        // Validate input files
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"Error: XFDF file not found at '{xfdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Open XFDF stream and import field values into the AcroForm
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                XfdfReader.ReadFields(xfdfStream, pdfDocument);
            }

            // Save the updated PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"XFDF data imported successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}