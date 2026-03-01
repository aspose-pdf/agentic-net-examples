using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string pclPath   = "input.pcl";   // PCL file to be opened
        const string pdfOutput = "output.pdf";  // Resulting PDF file

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Open the PCL file as a stream and load it using PclLoadOptions
        using (FileStream pclStream = File.OpenRead(pclPath))
        {
            // PclLoadOptions tells Aspose.Pdf to treat the input as PCL
            PclLoadOptions loadOptions = new PclLoadOptions();

            // Document(Stream, LoadOptions) constructor loads the PCL content and converts it to PDF internally
            using (Document pdfDoc = new Document(pclStream, loadOptions))
            {
                // Save the resulting PDF document
                pdfDoc.Save(pdfOutput);
            }
        }

        Console.WriteLine($"PCL file '{pclPath}' has been converted to PDF '{pdfOutput}'.");
    }
}