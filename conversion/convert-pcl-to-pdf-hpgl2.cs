using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PCL file and the destination PDF file
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Source file not found: {pclPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Create PCL load options. In recent Aspose.Pdf versions HP‑GL/2
        // vector loading is enabled by default, and the former EnableHPGL2
        // property has been removed. Therefore we instantiate the options
        // without setting that property.
        // -----------------------------------------------------------------
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Load the PCL file using the options. The Document constructor that
        // accepts a filename and LoadOptions performs the conversion.
        using (Document pdfDocument = new Document(pclPath, loadOptions))
        {
            // Save the resulting PDF. No SaveOptions are required because the
            // default PDF format is used.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PCL file '{pclPath}' successfully converted to PDF at '{pdfPath}'.");
    }
}
