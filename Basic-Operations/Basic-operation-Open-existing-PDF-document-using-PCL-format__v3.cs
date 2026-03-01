using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        // Verify that the source PCL file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Load options specific to PCL input
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Open the PCL file as a PDF document using the constructor that accepts LoadOptions
        using (Document pdfDocument = new Document(pclPath, loadOptions))
        {
            // Save the converted document as a PDF (PCL cannot be saved)
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PCL file successfully converted to PDF: {pdfPath}");
    }
}