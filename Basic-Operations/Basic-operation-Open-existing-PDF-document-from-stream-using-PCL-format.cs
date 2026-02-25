using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string outputPdf = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Open the PCL file as a read‑only stream
        using (FileStream pclStream = File.OpenRead(pclPath))
        {
            // Load options specific to PCL conversion
            PclLoadOptions pclOptions = new PclLoadOptions();

            // Create a PDF Document from the PCL stream using the load options
            using (Document pdfDoc = new Document(pclStream, pclOptions))
            {
                // Save the resulting PDF document
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"Converted PCL to PDF: {outputPdf}");
            }
        }
    }
}