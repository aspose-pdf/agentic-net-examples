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

        // Verify that the input file exists
        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPclPath}");
            return;
        }

        // Load the PCL file into a PDF Document using PclLoadOptions
        using (Document pdfDocument = new Document(inputPclPath, new PclLoadOptions()))
        {
            // Modify document properties via PdfFileInfo facade
            PdfFileInfo fileInfo = new PdfFileInfo(pdfDocument)
            {
                Title = "Modified PDF Title",
                Author = "John Doe",
                Subject = "Converted from PCL",
                Keywords = "PCL, PDF, Aspose"
            };

            // Save the modified document as PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PCL file converted and properties updated: {outputPdfPath}");
    }
}