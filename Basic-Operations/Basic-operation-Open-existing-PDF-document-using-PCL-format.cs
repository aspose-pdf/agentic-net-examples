using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Set up options for loading a PCL file
        PclLoadOptions pclOptions = new PclLoadOptions();

        // Open the PCL file as a PDF document using the specified options
        using (Document pdfDoc = new Document(pclPath, pclOptions))
        {
            // Save the converted document as PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PCL file successfully converted to PDF: '{pdfPath}'");
    }
}