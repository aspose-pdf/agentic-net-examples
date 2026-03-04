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

        // Load the PCL file using PclLoadOptions and convert it to PDF.
        using (Document doc = new Document(pclPath, new PclLoadOptions()))
        {
            // Save the resulting PDF document.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed: '{pclPath}' → '{pdfPath}'.");
    }
}