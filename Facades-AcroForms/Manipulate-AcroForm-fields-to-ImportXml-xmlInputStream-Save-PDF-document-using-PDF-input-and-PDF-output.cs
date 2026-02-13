using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input PDF, XML (XFDF) stream file, output PDF
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <input-pdf> <xfdf-xml> <output-pdf>");
            return;
        }

        string inputPdfPath = args[0];
        string xmlPath = args[1];
        string outputPdfPath = args[2];

        // Validate file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML (XFDF) file not found at '{xmlPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Open the XML (XFDF) stream and import field values into the AcroForm
                using (FileStream xmlStream = File.OpenRead(xmlPath))
                {
                    // Import field values from the XFDF stream
                    XfdfReader.ReadFields(xmlStream, pdfDocument);
                }

                // Save the modified PDF to the specified output path
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}