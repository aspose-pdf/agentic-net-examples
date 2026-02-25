using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, PclLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Input PCL file and desired output PDF file paths
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Initialize load options for PCL conversion
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        // Use a using block for deterministic disposal of the Document (lifecycle rule)
        using (Document pdfDocument = new Document(pclPath, pclLoadOptions))
        {
            // Save the converted document as PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PCL file '{pclPath}' successfully converted to PDF '{pdfPath}'.");
    }
}