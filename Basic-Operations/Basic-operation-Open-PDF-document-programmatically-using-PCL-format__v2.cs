using System;
using System.IO;
using Aspose.Pdf; // PclLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        const string pclPath   = "input.pcl";   // source PCL file
        const string pdfPath   = "output.pdf";  // destination PDF file

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        try
        {
            // Load the PCL file using PclLoadOptions.
            // The Document constructor that accepts (string, LoadOptions) performs the conversion.
            using (Document doc = new Document(pclPath, new PclLoadOptions()))
            {
                // Save the resulting PDF.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"PCL file converted and saved as PDF: '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}