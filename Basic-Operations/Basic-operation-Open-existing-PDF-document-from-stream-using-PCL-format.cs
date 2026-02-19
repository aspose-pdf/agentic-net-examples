using System;
using System.IO;
using Aspose.Pdf; // Document, PclLoadOptions

class Program
{
    static void Main()
    {
        // Path to the source PCL file and the destination PDF file.
        const string pclFilePath = "input.pcl";
        const string pdfOutputPath = "output.pdf";

        // Verify that the PCL file exists before attempting to open it.
        if (!File.Exists(pclFilePath))
        {
            Console.Error.WriteLine($"Error: PCL file not found at '{pclFilePath}'.");
            return;
        }

        // Open the PCL file as a read‑only stream.
        using (FileStream pclStream = File.OpenRead(pclFilePath))
        {
            // Create load options specific to PCL conversion.
            PclLoadOptions loadOptions = new PclLoadOptions();

            // Load the PCL content into an Aspose.Pdf Document using the stream and options.
            using (Document pdfDocument = new Document(pclStream, loadOptions))
            {
                // Save the resulting PDF document to the specified output path.
                pdfDocument.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"PCL file successfully converted to PDF: {pdfOutputPath}");
    }
}