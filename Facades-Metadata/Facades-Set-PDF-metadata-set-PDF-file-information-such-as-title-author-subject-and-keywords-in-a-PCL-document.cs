using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PCL file and output PDF file paths
        const string inputPclPath = "input.pcl";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PCL file exists
        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPclPath}");
            return;
        }

        // Load the PCL file and convert it to a PDF document
        Document pdfDocument = new Document(inputPclPath, new PclLoadOptions());

        // Create a PdfFileInfo facade bound to the loaded document
        PdfFileInfo pdfInfo = new PdfFileInfo(pdfDocument);

        // Set the desired metadata properties
        pdfInfo.Title = "Sample PDF Title";
        pdfInfo.Author = "John Doe";
        pdfInfo.Subject = "Demonstration of setting PDF metadata via Aspose.Pdf.Facades";
        pdfInfo.Keywords = "Aspose.Pdf, Facades, Metadata, PCL";

        // Save the PDF document with the updated metadata
        pdfInfo.Save(outputPdfPath);

        Console.WriteLine($"PDF saved successfully to '{outputPdfPath}' with updated metadata.");
    }
}