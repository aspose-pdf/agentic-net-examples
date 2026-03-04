using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, PclLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Path to the source PCL file
        const string pclPath = "input.pcl";
        // Path where the resulting PDF will be saved
        const string pdfPath = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Open the PCL file as a read‑only stream
        using (FileStream pclStream = File.OpenRead(pclPath))
        {
            // Initialize load options for PCL input
            PclLoadOptions pclLoadOptions = new PclLoadOptions();

            // Load the PCL stream and convert it to a PDF Document
            using (Document pdfDoc = new Document(pclStream, pclLoadOptions))
            {
                // Save the resulting PDF
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"PCL file '{pclPath}' has been converted to PDF '{pdfPath}'.");
    }
}