using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PCL file (could be any stream source)
        const string pclPath = "input.pcl";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Open the PCL file as a stream
        using (FileStream pclStream = File.OpenRead(pclPath))
        {
            // Prepare load options for PCL conversion
            PclLoadOptions pclLoadOptions = new PclLoadOptions();

            // Load the PCL stream into a PDF Document
            using (Document pdfDoc = new Document(pclStream, pclLoadOptions))
            {
                // Save the resulting PDF document
                pdfDoc.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"PCL file converted and saved as PDF: '{pdfOutputPath}'");
    }
}