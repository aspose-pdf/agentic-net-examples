using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // XfdfReader resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string xfdfPath      = "data.xfdf";   // XFDF containing field values
        const string outputPdfPath = "output.pdf";  // updated PDF

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"Error: XFDF file not found – {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Open the XFDF stream and import its field values into the PDF
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    // XfdfReader.ReadFields reads the XFDF data and populates the form fields
                    XfdfReader.ReadFields(xfdfStream, pdfDoc);
                }

                // Save the modified document back to PDF format
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully updated PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}