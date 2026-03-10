using System;
using System.IO;
using Aspose.Pdf; // Document, PclLoadOptions

class Program
{
    static void Main()
    {
        const string pclInputPath  = "input.pcl";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(pclInputPath))
        {
            Console.Error.WriteLine($"File not found: {pclInputPath}");
            return;
        }

        // Open the PCL file as a stream and load it with PclLoadOptions.
        using (FileStream pclStream = File.OpenRead(pclInputPath))
        {
            PclLoadOptions pclOptions = new PclLoadOptions(); // default options

            // Document constructor that accepts a stream and LoadOptions.
            using (Document pdfDoc = new Document(pclStream, pclOptions))
            {
                // Save the resulting PDF.
                pdfDoc.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"PCL file converted and saved as PDF: '{pdfOutputPath}'");
    }
}