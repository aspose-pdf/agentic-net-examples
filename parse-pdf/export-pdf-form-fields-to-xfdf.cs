using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "output.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document using a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Initialize the Form facade with the loaded document (no using directive for Aspose.Pdf.Facades)
                Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form(doc);

                // Create a FileStream to write the XFDF data
                using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
                {
                    // Export form fields to XFDF
                    formFacade.ExportXfdf(xfdfStream);
                } // xfdfStream is closed here

                // No need to save the document for XFDF export
                Console.WriteLine($"XFDF data exported to '{outputXfdfPath}'.");
            } // doc is disposed here
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}