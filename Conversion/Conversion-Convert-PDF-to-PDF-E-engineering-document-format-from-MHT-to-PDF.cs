using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory that contains the source MHT file and where the PDF will be saved.
        // Use the current working directory for simplicity.
        string dataDir = Directory.GetCurrentDirectory();

        // Input MHT file.
        string mhtFile = Path.Combine(dataDir, "sample.mht");

        // Output PDF file.
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        // Verify that the source file exists.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{mhtFile}'.");
            return;
        }

        try
        {
            // Initialize load options for MHT format.
            MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

            // Load the MHT document using the specified options.
            using (Document pdfDocument = new Document(mhtFile, mhtLoadOptions))
            {
                // NOTE: The PdfEConformance property is not available in the current
                // Aspose.Pdf version used for this project. If PDF/E conformance is
                // required, upgrade to a version that supports it and set the property
                // accordingly. For now we simply save the document as a regular PDF.

                // Save the document as PDF.
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"MHT file successfully converted to PDF at '{pdfFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
