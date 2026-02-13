using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // XfdfReader resides here

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputPdfPath> <xfdfPath> <outputPdfPath>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputPdfPath> <xfdfPath> <outputPdfPath>");
            return;
        }

        string inputPdfPath = args[0];
        string xfdfPath = args[1];
        string outputPdfPath = args[2];

        // Verify that the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"Error: XFDF file not found – {xfdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDoc = new Document(inputPdfPath);

            // Open the XFDF file as a stream and import field values
            using (Stream xfdfStream = File.OpenRead(xfdfPath))
            {
                XfdfReader.ReadFields(xfdfStream, pdfDoc);
            }

            // Save the updated PDF document
            pdfDoc.Save(outputPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An exception occurred: {ex.Message}");
        }
    }
}