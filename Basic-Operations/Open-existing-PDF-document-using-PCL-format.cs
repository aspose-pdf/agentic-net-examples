using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PCL file
        const string pclPath = "input.pcl";

        // Path where the resulting PDF will be saved
        const string pdfPath = "output.pdf";

        // Verify that the PCL file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Initialize load options for PCL conversion
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Open the PCL file and convert it to a PDF document
        using (Document pdfDocument = new Document(pclPath, loadOptions))
        {
            // Save the document as PDF (extension determines format)
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PCL file converted and saved as PDF: {pdfPath}");
    }
}